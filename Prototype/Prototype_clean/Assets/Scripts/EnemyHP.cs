using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int HP;
    public float knockbackForce;
    private Animator anim;
    private ParticleSystem ParticleSys;

    private Rigidbody2D rb2d;

    private bool invincible;

    private Vector3 knockBackDirection;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ParticleSys = GetComponent<ParticleSystem>();
        rb2d = GetComponent<Rigidbody2D>();
        invincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     if (collision.gameObject.tag == "attackBox")
        {
            if (!invincible)
            {
                HP--;
                ParticleSys.Play();
                invincible = true;
                StartCoroutine(invincibleTimer());
                knockBackDirection = rb2d.transform.position - collision.transform.position;
                rb2d.AddForce(knockBackDirection.normalized * knockbackForce);
            }
        }
    }

    //Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            ParticleSys.Play();
            gameObject.SetActive(false);
        }
    }

    public IEnumerator invincibleTimer()
    {

        yield return new WaitForSeconds(1f);
        invincible = false;
    }
}
