using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Backup : MonoBehaviour
{
    public float jumpForce, speed, MaxJump, holdTime;  // 
    private bool canJump = false;

    private Rigidbody2D rb2d;
    private AudioSource source;
    public AudioClip jumpSound;
    public GameManager GameMan;
    private Transform transF;
    private Animator anim;

    private SpriteRenderer spriteRen;

    private Vector3 respawnPos;

    private float moveHorizontal, moveVertical;

    private bool invincible;

    public static bool firing;

    private int HP;

    private Color Black = new Color(0f, 0f, 0f, 1f);
    private Color White = new Color(1f, 1f, 1f, 1f);


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     //get rigidbody
        source = GetComponent<AudioSource>();
        transF = GetComponent<Transform>();
        spriteRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        GameMan = GameObject.Find("GameManager").GetComponent<GameManager>();

        invincible = false;
        resetHP();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) GameMan.PauseGame();
    }
    
 

    // Update is called once per frame
    void FixedUpdate()
    {
        //this set of code is for the basic left/right movement of the player character
        moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal < 0) anim.SetBool("isRight", false);
        if (moveHorizontal > 0) anim.SetBool("isRight", true);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !firing)
        {
            takeDamage();
        }
    }

    //public void bounceMovement()
    //{
    //    rb2d.AddForce(new Vector2(0f, (jumpForce / 3f)), ForceMode2D.Impulse);
    //}

    void resetHP()
    {
        HP = 3;
        GameMan.hp = 3;
    }

    void takeDamage()
    {   

        if (!invincible && HP > 0)
        {
            HP--;
            GameMan.hp = HP;
            invincible = true;
            anim.SetBool("takeDamage", true);
            StartCoroutine(damageFlash());
        }
    
        if (HP <= 0)
        {

            getRespawn();
            transform.position = respawnPos;
            resetHP() ;
        }
               
    }


    void getRespawn()
    {
        respawnPos.x = PlayerPrefs.GetFloat("respawn X");
        respawnPos.y = PlayerPrefs.GetFloat("respawn Y");
        respawnPos.z = PlayerPrefs.GetFloat("respawn Z");
    }

    public IEnumerator damageFlash()
    {
        yield return new WaitForSeconds(1.5f);
        invincible = false;
        anim.SetBool("takeDamage", false);
    }
}
