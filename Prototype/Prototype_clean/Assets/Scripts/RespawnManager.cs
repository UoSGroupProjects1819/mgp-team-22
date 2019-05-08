using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 currentRespawn;
    private RotateManager rotMan;
    private Movement playerCon;
    private Transform lastCheckPoint;
    private Rigidbody2D rb2d;
    private bool onCooldown = false;

    private float gravReset;


    private void Start()
    {
        currentRespawn = transform.position;
        playerTransform = GetComponent<Transform>();
        rotMan = GameObject.FindObjectOfType<RotateManager>();
        playerCon = GetComponent<Movement>();
        rb2d = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
  //          if (playerCon.grounded == true)
  //          {
                // save respawn
                PlayerPrefs.SetFloat("respawn X", transform.position.x);
                PlayerPrefs.SetFloat("respawn Y", transform.position.y);
                PlayerPrefs.SetFloat("respawn Z", transform.position.z);
                PlayerPrefs.SetString("SpawnTarget", "Checkpoint");

                // save rotation
                rotMan.SaveGrav();

   //         }

            //currentRespawn = transform.position;
            lastCheckPoint = collision.gameObject.transform;
         //   print("spawn saved");
        }

        if (collision.gameObject.tag == "fallDeath")
        {
       //     StartCoroutine(freezeTime());
            rotMan.LoadGrav();
            rotMan.SetGrav();
            playerTransform.position = lastCheckPoint.position;
            rb2d.velocity = new Vector3(0, 0, 0);
            //  print("death triggered");
        }

        //if (collision.gameObject.tag == "Teleporter")
        //{
        //    PlayerPrefs.SetString("SpawnTarget", "Teleporter");
        //}
    }

    public IEnumerator freezeTime()
    {
     //   gravReset = rb2d.gravityScale;
        rb2d.gravityScale = 0;
        rb2d.velocity = new Vector3(0,0,0);
        yield return new WaitForSeconds(3F);
        rb2d.gravityScale = 3;
    }
}
