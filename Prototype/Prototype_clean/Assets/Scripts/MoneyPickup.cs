using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyPickup : MonoBehaviour
    
{
    public GameManager gameMan;
    public Text MoneyText;
    public int MoneyCount;

    void OnTriggerEnter2D(Collider2D trigger)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "Money", if it is...
        if (trigger.gameObject.CompareTag("Money"))
        {
            trigger.gameObject.SetActive(false);

            MoneyCount = MoneyCount + 1;

            SetMoneyText();
        }
    }

    void SetMoneyText()
    {
        MoneyText.text = "Coins: " + MoneyCount.ToString();
    }
}
