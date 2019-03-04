using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
   
{
    public UIManager UIMan;
    //static GameObject uiManRef;
    public Vector2 gravity;

    public int hp;

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

        Physics2D.gravity = gravity;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
