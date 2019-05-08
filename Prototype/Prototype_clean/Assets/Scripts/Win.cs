using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameObject fadeBox, credits, titleText;
    public float fadeTime, textTime, creditsTime, restartTime;
    private float fadeAlpha = 1;
    private Image fader;
    private Color colour;


    public bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        fader = fadeBox.GetComponent<Image>();
        colour = fader.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hasWon = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if we have won
        if (hasWon)
        {
            //timer for fade
            if (fadeTime > 0)
            {
                fadeTime -= Time.deltaTime;
                colour.a = 1 - fadeTime;
                fader.color = colour;
            }

            if (fadeTime <= 0)
            {
                // if fade finished, timer for credits
                if (creditsTime > 0)
                {
                    credits.SetActive(true);
                    creditsTime -= Time.deltaTime;
                }

                if (creditsTime <= 0)
                {

                    // if credits finished, timer for title
                    titleText.SetActive(true);

                    restartTime -= Time.deltaTime;
                    if (restartTime <= 0) SceneManager.LoadScene("Main Menu");
                }
            }
   




            // if credits finished timer for load main menu

        }
    }
}
