using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject MenuCanvas;
    public Button NewGameBut;
    public Button LoadGameBut;
    public Button ExitBut;

    private GameManager gameMan;
    public string ArtifactTrigger1, ArtifactTrigger2;
    public void SetGameManager(GameManager game)
    {
        gameMan = game;
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt(ArtifactTrigger1, 0);
        PlayerPrefs.SetInt(ArtifactTrigger2, 0);
        PlayerPrefs.SetString("SpawnTarget", "Home");
        SceneManager.LoadScene("World1");

    }


    

    






}
