using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame ()
    {
        PlayerPrefs.SetString("SpawnTarget", "Resume");
        SceneManager.LoadScene("World1");
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
