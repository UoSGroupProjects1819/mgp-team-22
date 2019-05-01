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

    private void Start()
    {
        currentRespawn = transform.position;
        playerTransform = GetComponent<Transform>();
        rotMan = GameObject.FindObjectOfType<RotateManager>();
        playerCon = GetComponent<Movement>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            if (playerCon.grounded == true)
            {
                PlayerPrefs.SetFloat("respawn X", transform.position.x);
                PlayerPrefs.SetFloat("respawn Y", transform.position.y);
                PlayerPrefs.SetFloat("respawn Z", transform.position.z);
                PlayerPrefs.SetString("SpawnTarget", "Checkpoint");
            }

            //currentRespawn = transform.position;
            lastCheckPoint = collision.gameObject.transform;
            print("spawn saved");
        }

        if (collision.gameObject.tag == "fallDeath")
        {

            playerTransform.position = lastCheckPoint.position;
            //rotMan.Rotate();
            //  print("death triggered");
        }

        //if (collision.gameObject.tag == "Teleporter")
        //{
        //    PlayerPrefs.SetString("SpawnTarget", "Teleporter");
        //}
    }

}
