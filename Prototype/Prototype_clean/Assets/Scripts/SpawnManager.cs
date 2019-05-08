using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private Transform playerTransform;
    public Transform homeLocation, TeleporterLocation;



    private Vector3 HomePos, TelPos, respawnPos;

    private string spawnTarget;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        spawnTarget = PlayerPrefs.GetString("SpawnTarget");
        
        HomePos = homeLocation.position;
        TelPos = TeleporterLocation.position;


        switch (spawnTarget)
        {

            case "Home":
                playerTransform.position = HomePos;
                PlayerPrefs.SetFloat("respawn X", transform.position.x);
                PlayerPrefs.SetFloat("respawn Y", transform.position.y);
                PlayerPrefs.SetFloat("respawn Z", transform.position.z);
                return;

            case "Teleporter":

                playerTransform.position = TelPos;
                PlayerPrefs.SetFloat("respawn X", transform.position.x);
                PlayerPrefs.SetFloat("respawn Y", transform.position.y);
                PlayerPrefs.SetFloat("respawn Z", transform.position.z);
                return;

            case "Checkpoint":
                if (Time.time > 1)
                {

                    respawnPos.x = PlayerPrefs.GetFloat("respawn X");
                    respawnPos.y = PlayerPrefs.GetFloat("respawn Y");
                    respawnPos.z = PlayerPrefs.GetFloat("respawn Z");
                    playerTransform.position = respawnPos;
                }
                return;

            case "Resume":
                    respawnPos.x = PlayerPrefs.GetFloat("respawn X");
                    respawnPos.y = PlayerPrefs.GetFloat("respawn Y");
                    respawnPos.z = PlayerPrefs.GetFloat("respawn Z");
                    playerTransform.position = respawnPos;
                
                return;

        }


    }

 

    // Update is called once per frame
    void Update()
    {
        
    }
}
