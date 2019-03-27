using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyPickup : MonoBehaviour
    
{
    public GameManager gameMan;
    public Text MoneyText;
    public int MoneyCount;

    public PlayerController_Backup playerCon;

    private void Start()
    {
        playerCon = GetComponent<PlayerController_Backup>();
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "Money", if it is...
        if (trigger.gameObject.CompareTag("Money"))
        {
            if (playerCon.firing != true)
            {
                trigger.gameObject.SetActive(false);

                MoneyCount = MoneyCount + 1;

                SetMoneyText();
            }
        }
    }

    void SetMoneyText()
    {
        print(MoneyCount);
        MoneyText.text = "Coins: " + MoneyCount.ToString();
    }
}
