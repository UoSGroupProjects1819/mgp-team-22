using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyPickup : MonoBehaviour
    
{
    public GameManager gameMan;
    public Text MoneyText;
    public int MoneyCount;

    private bool hasPicked;

    public PlayerController_Backup playerCon;

    private void Start()
    {
        playerCon = GetComponent<PlayerController_Backup>();
        MoneyCount = PlayerPrefs.GetInt("Money");
        SetMoneyText();

        hasPicked = false;

    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "Money", if it is...
        if (trigger.gameObject.CompareTag("Money"))
        {
            if (playerCon.firing != true)
            {
                trigger.gameObject.SetActive(false);


                if (!hasPicked)
                {
                    MoneyCount = MoneyCount + 1;
                    hasPicked = true;
                }


                SetMoneyText();
            }
        }
    }

    private void Update()
    {
        hasPicked = false;
    }

    void SetMoneyText()
    {
     //   print(MoneyCount);
        MoneyText.text = " " + MoneyCount.ToString();
    }
}
