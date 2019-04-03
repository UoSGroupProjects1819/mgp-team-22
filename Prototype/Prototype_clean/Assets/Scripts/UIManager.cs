using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject PauseCanvas;
    public GameObject HealthSprite1;
    public GameObject HealthSprite2;
    public GameObject HealthSprite3;

    public GameObject player;   //added for debug commands

    public Button Resume;
    private GameManager gameMan;

    public Text hpText;

    public string ArtifactTrigger1, ArtifactTrigger2, ArtifactTrigger3;

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

    public void ResetGame()
    {
        PlayerPrefs.SetInt(ArtifactTrigger1, 0);
        PlayerPrefs.SetInt(ArtifactTrigger2, 0);
        PlayerPrefs.SetInt(ArtifactTrigger3, 0);
        PlayerPrefs.SetString("SpawnTarget", "Home");

        PlayerPrefs.SetInt("Money", 0);
    }
   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ToggleNoClip()
    {
        player.GetComponent<CircleCollider2D>().enabled = !player.GetComponent<CircleCollider2D>().enabled;
    }

    public void ToggleFlyMode()
    {
        player.GetComponent<Movement>().ToggleFly();
    }


    // Update is called once per frame
    void Update()
    {
        switch(gameMan.hp)
        {
            case 1:
                HealthSprite1.SetActive(true);
                HealthSprite2.SetActive(false);
                HealthSprite3.SetActive(false);
                break;

            case 2:
                HealthSprite1.SetActive(true);
                HealthSprite2.SetActive(true);
                HealthSprite3.SetActive(false);
                break;

            case 3:
                HealthSprite1.SetActive(true);
                HealthSprite2.SetActive(true);
                HealthSprite3.SetActive(true);
                break;

        }
    }
}
