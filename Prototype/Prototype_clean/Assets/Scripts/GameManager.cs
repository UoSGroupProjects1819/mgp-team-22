﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
   
{
    public UIManager UIMan;
    //static GameObject uiManRef;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        UIMan.OpenMenu();
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIMan = GameObject.Find("UIManager").GetComponent<UIManager>();
        UIMan.SetGameManager(this);
        // uiManRef = UIMan;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
