using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private Transform playerTransform;
    public Transform homeLocation, TeleporterLocation;

    private Vector3 HomePos, TelPos;

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
                return;

            case "Teleporter":
                playerTransform.position = TelPos;
                return;

        }


    }

 

    // Update is called once per frame
    void Update()
    {
        
    }
}
