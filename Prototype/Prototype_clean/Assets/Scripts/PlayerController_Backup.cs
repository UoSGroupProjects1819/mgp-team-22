using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Backup : MonoBehaviour
{
    public float jumpForce, speed;  // removed default to be editable in unity
    private bool grounded = false;   
    //public bool jump;             // removed to simplify
    //public Transform groundCheck; // removed to simplify

    private Rigidbody2D rb2d;
    private float floorY, currentY;
   

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     //get rigidbody
        currentY = rb2d.transform.position.y;   //set currentY as the currentY value of the player
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //this set of code is for the basic left/right movement of the player character
        float moveHorizontal = Input.GetAxis("Horizontal");

        //directly sets velocity of Actor, this is tighter than adding a force
        //and means if you stop holding a direction movement stops instantly
        //uses deltaTime to prevent performance variation between computers
        rb2d.velocity = new Vector2 ((moveHorizontal * speed * Time.deltaTime), rb2d.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // checks if the player has collided with the ground or an enemy
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        // checks if the player has left collision  with the ground or an enemy
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            grounded = false;
        }
    }

    void Jump()
    {

        if (grounded) //checks the player is grounded
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
