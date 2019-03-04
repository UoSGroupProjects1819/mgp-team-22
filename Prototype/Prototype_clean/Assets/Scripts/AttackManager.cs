using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public float range;
    private float inputH, inputV;
    private bool canAttack, downAttack;

    public float attackDuration;

    private string inputD;

    public GameObject up, down, side;
    public PlayerController_Backup playerCont;

    public IEnumerator attackTime()
    {
        yield return new WaitForSeconds(attackDuration);
        up.gameObject.SetActive(false);
        down.gameObject.SetActive(false);
        side.gameObject.SetActive(false);
        canAttack = true;
        downAttack = false;

    }

    // Update is called once per frame

    private void Start()
    {
        canAttack = true;
        downAttack = false;
        playerCont = GameObject.Find("PlayerCharacter").GetComponent<PlayerController_Backup>();
    }

    void Update()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        if (inputV * inputV < inputH * inputH)
        {
            if (inputH < 0) inputD = "left";
            
            if (inputH > 0) inputD = "right";
            
        }

        else
        {
            if (inputV > 0) inputD = "up";
            
            if (inputV < 0) inputD = "down";
        }

        if (Input.GetButtonDown("Fire1") && canAttack) Attack(inputD);
        
    }

    void Attack(string input)
    {
        StartCoroutine(attackTime());
        canAttack = false;

        switch (input)
        {
            case "up":
                up.gameObject.SetActive(true);
                return;

            case "down":
                down.gameObject.SetActive(true);
                downAttack = true;
                return;

            case "left":
                side.gameObject.SetActive(true);
                return;

            case "right":
                side.gameObject.SetActive(true);
                return;
        }

       
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !canAttack)
        {
            if (downAttack)
            {
                print("bounce");
                playerCont.bounceMovement();

            }

         //   else collision.gameObject.SetActive(false);

        }
          
    }
}
