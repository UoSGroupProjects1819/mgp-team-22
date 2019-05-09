using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private string DestinationWorld;
    public void PlayGame ()
    {
        PlayerPrefs.SetString("SpawnTarget", "Resume");
        DestinationWorld = PlayerPrefs.GetString("SpawnWorld");
        SceneManager.LoadScene(DestinationWorld);
    }

    public void NewGame()
    {
        PlayerPrefs.SetString("SpawnTarget", "Home");
        SceneManager.LoadScene("World1");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void ThirdBtn()
    {


    }
}
