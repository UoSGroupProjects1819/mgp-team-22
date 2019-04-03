using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int HP;
    public float knockbackForce, coinForce;
    private Animator anim;
    private ParticleSystem ParticleSys;
    public DroppedMoney DropMon;
    public int CoinNumber;
    
    private Vector3 leftLeft, left, right, rightRight;
    
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
        DropMon = GameObject.Find("MoneyManager").GetComponent<DroppedMoney>();

        leftLeft = new Vector3(-1, 1, 0);
        left = new Vector3(-0.5f, 1, 0);
        rightRight = new Vector3(1, 1, 0);
        right = new Vector3(0.5f, 1, 0);
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

            foreach (GameObject coin in DropMon.coins)
            {
                if (coin.activeSelf == false)
                {
                    if (CoinNumber > 0)
                    {
                        Rigidbody2D rbTemp;
                        Vector3 forceDirection;
                        forceDirection = GetDirection();
                        coin.transform.position = transform.position;
                        coin.SetActive(true);
                        rbTemp = coin.GetComponent<Rigidbody2D>();
                        rbTemp.AddForce(forceDirection * coinForce);
                        CoinNumber--;
                    }

                    if (CoinNumber <= 0)
                    {
                        break;
                    }

                }
            }
            

            gameObject.SetActive(false);
        }
    }

    public IEnumerator invincibleTimer()
    {

        yield return new WaitForSeconds(1f);
        invincible = false;
    }

    private Vector3 GetDirection()
    {
        int temp = Random.Range(0, 4);

        switch (temp)
        {
            case 0:
                return leftLeft;

            case 1:
                return left;

            case 2:
                return right;

            case 3:
                return rightRight;

        }

        return leftLeft;
    }
}
