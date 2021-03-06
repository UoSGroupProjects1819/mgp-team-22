﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{

    public string Destination, SpawnTargetName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetFloat("respawn X", transform.position.x);
            PlayerPrefs.SetFloat("respawn Y", transform.position.y);
            PlayerPrefs.SetFloat("respawn Z", transform.position.z);

            PlayerPrefs.SetString("SpawnTarget", SpawnTargetName);
            PlayerPrefs.SetString("SpawnWorld", Destination);
            SceneManager.LoadScene(Destination);

        }



    }
    




}
