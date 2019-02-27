using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPrefs : MonoBehaviour
{
    int Value;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Score", 1);
        SetText();
    }

    void SetText()
    {
       Value = PlayerPrefs.GetInt("Value", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Value = +1;

        }
    }
}
