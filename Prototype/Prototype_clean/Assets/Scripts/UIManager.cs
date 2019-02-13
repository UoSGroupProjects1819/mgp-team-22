using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject PauseCanvas;

    public Button Resume;
    private GameManager gameMan;

    public void SetGameManager(GameManager game)
    {
        gameMan = game;
    }



    public void OpenMenu()
    {
        PauseCanvas.SetActive(true);
    }
    public void CloseMenu()
    {
        PauseCanvas.SetActive(false);
        gameMan.UnPauseGame();
    }

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
