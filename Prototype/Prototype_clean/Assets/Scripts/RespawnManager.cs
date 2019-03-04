using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 currentRespawn;

    private void Start()
    {
        currentRespawn = transform.position;
        playerTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            PlayerPrefs.SetFloat("respawn X", transform.position.x);
            PlayerPrefs.SetFloat("respawn Y", transform.position.y);
            PlayerPrefs.SetFloat("respawn Z", transform.position.z);
            PlayerPrefs.SetString("SpawnTarget", "Checkpoint"); 

            currentRespawn = transform.position;
          //  print("spawn saved");
        }

        if (collision.gameObject.tag == "fallDeath")
        {
            playerTransform.position = currentRespawn;
          //  print("death triggered");
        }
    }

}
