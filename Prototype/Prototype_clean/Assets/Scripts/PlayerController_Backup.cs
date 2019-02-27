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

    private float floorY, JumpTimer, holdTimer;
    private float moveHorizontal, moveVertical;

    private bool jumping;
   

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     //get rigidbody
        source = GetComponent<AudioSource>();
        transF = GetComponent<Transform>();

        GameMan = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && canJump) jumping = true;
        
        if (Input.GetButtonUp ("Jump")) jumping = false;

        if (Input.GetButtonDown("Cancel")) GameMan.PauseGame();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //this set of code is for the basic left/right movement of the player character
        moveHorizontal = Input.GetAxis("Horizontal");

        if (jumping && JumpTimer <= MaxJump)
        {
            moveVertical = jumpForce * Time.deltaTime;
            JumpTimer += Time.deltaTime;

            if (source.isPlaying == false) source.PlayOneShot(jumpSound, 0.5f);

        }

        else if (!jumping && holdTimer <= holdTime)
        {
            moveVertical = 0f;
            holdTimer += Time.deltaTime;
        }

        else
        {
            moveVertical = Physics2D.gravity.y * 0.8f;
            jumping = false;
        }


        //directly sets velocity of Actor, this is tighter than adding a force
        //and means if you stop holding a direction movement stops instantly
        //uses deltaTime to prevent performance variation between computers
        rb2d.AddForce(new Vector2(0f, moveVertical), ForceMode2D.Impulse);
        rb2d.velocity = new Vector2((moveHorizontal * speed * Time.deltaTime), rb2d.velocity.y);

        if (moveHorizontal < 0) transF.localScale = new Vector2(1f ,transF.localScale.y);

        if (moveHorizontal > 0) transF.localScale = new Vector2(-1f,transF.localScale.y);

    }



        private void OnTriggerEnter2D(Collider2D collision)
    {

        // checks if the player has collided with the ground or an enemy
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            canJump = true;
            JumpTimer = 0f;
            holdTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        // checks if the player has left collision  with the ground or an enemy when jumping (not walking off)
        if (jumping && (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy"))
        {
            canJump = false;
        }
    }

    public void bounceMovement()
    {
        rb2d.AddForce(new Vector2(0f, (jumpForce/3f)),ForceMode2D.Impulse);
    }
}
