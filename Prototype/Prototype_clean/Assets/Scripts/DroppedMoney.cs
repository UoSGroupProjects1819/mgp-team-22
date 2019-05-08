using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedMoney : MonoBehaviour
{
    public GameObject coin;
    public int PooledAmount = 50;
    public List<GameObject> coins;

    void Start()
    {
        coins = new List<GameObject>();
        for (int i = 0; i < PooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(coin);
            obj.SetActive(false);
            coins.Add(obj);
        }

    }

    void Drop()
    {
        for (int i = 0; i < coins.Count; i++)
        {
            if (!coins[i].activeInHierarchy)
            {
                coins[i].transform.position = transform.position;
                coins[i].transform.rotation = transform.rotation;
                coins[i].SetActive(true);
                break;

            }

        }

    }
}
