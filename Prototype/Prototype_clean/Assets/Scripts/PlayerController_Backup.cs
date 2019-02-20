﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Backup : MonoBehaviour
{
    public float jumpForce, speed, MaxJump, holdTime;  // 
    private bool canJump = false;   
    //public bool jump;             // removed to simplify
    //public Transform groundCheck; // removed to simplify

    private Rigidbody2D rb2d;
    private float floorY, currentY, JumpTimer, holdTimer;
    private float moveHorizontal, moveVertical;

    private bool jumping;
   

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     //get rigidbody
        currentY = rb2d.transform.position.y;   //set currentY as the currentY value of the player
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && canJump) jumping = true;
        
        if (Input.GetButtonUp ("Jump")) jumping = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //this set of code is for the basic left/right movement of the player character
        moveHorizontal = Input.GetAxis("Horizontal");

        if (jumping && JumpTimer <= MaxJump)
        {
            moveVertical = jumpForce * Time.deltaTime;
            JumpTimer += Time.deltaTime;

        }

        else if (!jumping && holdTimer <= holdTime)
        {
            moveVertical = 0f;
            holdTimer += Time.deltaTime;
        }

        else
        {
            moveVertical = Physics2D.gravity.y * 0.8f;
            jumping = false;
        }


       // if (JumpTimer > MaxJump) jump


        //directly sets velocity of Actor, this is tighter than adding a force
        //and means if you stop holding a direction movement stops instantly
        //uses deltaTime to prevent performance variation between computers
        rb2d.AddForce (new Vector2(0f, moveVertical), ForceMode2D.Impulse);
        rb2d.velocity = new Vector2 ((moveHorizontal * speed * Time.deltaTime), rb2d.velocity.y);

   
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("TRIGGERED");

        // checks if the player has collided with the ground or an enemy
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            canJump = true;
            JumpTimer = 0f;
            holdTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        // checks if the player has left collision  with the ground or an enemy
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            canJump = false;
        }
    }

    //void Jump()
    //{

    //    if (JumpTimer <= MaxJump) //checks the player is grounded
    //    {
    //        rb2d.AddForce(new Vector2(0f, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
    //        JumpTimer += Time.deltaTime;
    //    }
    //}

 }
