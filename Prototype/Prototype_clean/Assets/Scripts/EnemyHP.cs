using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int HP;
    private Animator anim;
    private ParticleSystem ParticleSys;

    private bool invincible;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ParticleSys = GetComponent<ParticleSystem>();
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
                StartCoroutine(damageFlash());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
     if (HP ==0)
        {
            ParticleSys.Play();
            this.gameObject.SetActive(false);
        }
    }

    public IEnumerator damageFlash()
    {

        yield return new WaitForSeconds(1f);
        invincible = false;
    }
}
