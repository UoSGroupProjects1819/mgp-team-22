﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingMovement : MonoBehaviour
{

    Rigidbody2D rigidBody;

    [Header("Enemy Types")]
    public bool bouncer;
    public bool chaser;
    [Header("Movement Config")]
    public float moveAcceleration;
    public float moveSpeed;
    public bool isMovementRangeLimited;
    public float moveBoundaryMaxX;
    public float moveBoundaryMaxY;
    public Vector3 moveBoundaryOrigin;
    [Header("Runtime Variables")]
    public bool chasing;
    public Vector2 direction;
   


    public GameObject player;
    

    //for editor usability, show boundaries of area the enemy can move if restricted
    void OnDrawGizmosSelected()
    {
        if (isMovementRangeLimited)
        {
            // Draw a semi transparent red square
            Gizmos.color = new Color(1, 0, 0, 0.3f);
            Gizmos.DrawCube(transform.position + moveBoundaryOrigin, new Vector2(moveBoundaryMaxX, moveBoundaryMaxY));

            if (moveBoundaryOrigin.x != 0 || moveBoundaryOrigin.y != 0)
            {
                Gizmos.DrawIcon(transform.position + moveBoundaryOrigin, "Start Point Gizmo.png", true);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        moveBoundaryOrigin = transform.position + moveBoundaryOrigin;   //allows for offset on movement range box centre


        rigidBody = GetComponent<Rigidbody2D>();
        if (bouncer)
        {
            direction = new Vector2(1, 1);                          //by default we start at an angle so the collisions bounce nicely
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(gameObject.transform.localPosition, new Vector3(gameObject.transform.localPosition.x + direction.x,
            gameObject.transform.localPosition.y + direction.y, gameObject.transform.localPosition.z), Color.red);
    }

    private void FixedUpdate()
    {
        if(chasing)
        {
            if (transform.position.x < player.transform.position.x) //move towards the player on x
            {
                direction.x = 1;
            }
            else
            {
                direction.x = -1;
            }
            if (transform.position.y < player.transform.position.y) //and on y
            {
                direction.y = 1;
            }
            else
            {
                direction.y = -1;
            }
        }

        //apply movement
        rigidBody.AddForce(direction*moveAcceleration);

        if (rigidBody.velocity.magnitude > moveSpeed)       //clamps move speed to a maximum
        {
            rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
        }

        if (isMovementRangeLimited)
        {
            //enemy leaves its restricted range. this code is disgustingly huge. refactor at will
            if (transform.position.x > moveBoundaryOrigin.x + (moveBoundaryMaxX / 2))  //this is really clunky. interface uses box radius, while code has to use diameter. hence /2
            {
                if (bouncer)
                {
                    direction.x = -1;
                }
                else
                {
                    chasing = false;
                }
            }
            if (transform.position.x < moveBoundaryOrigin.x - (moveBoundaryMaxX / 2))   //less than min x
            {
                if (bouncer)
                {
                    direction.x = 1;
                }
                else
                {
                    chasing = false;
                }
            }
            if (transform.position.y > moveBoundaryOrigin.y + (moveBoundaryMaxY / 2))   //more than max y
            {
                if (bouncer)
                {
                    direction.y = -1;
                }
                else
                {
                    chasing = false;
                }
            }
            if (transform.position.y < moveBoundaryOrigin.y - (moveBoundaryMaxY / 2))   //less than min y
            {
                if (bouncer)
                {
                    direction.y = 1;
                }
                else
                {
                    chasing = false;
                }
            }
        }

    }

    public bool colCheck = true;    //variable is true on creation because i'm trying to let it run once then prevent it

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy") && colCheck)
        {
            colCheck = false;
            StartCoroutine(ColCheckRefresh(0.1f));      //delay of 0.1 seconds between collision checks, fixed a bug when colliding two wall parts at the same time.
                                                        //this introduces a new bug when the enemy perfectly collides with both sides on a corner simultaneously, but this
                                                        //is much less frequent.

            //Debug.Log(collision.GetContact(0).normal);
            if (collision.GetContact(0).normal.x != 0.0f)   //hits floor or ceiling
            {
                direction.x = -direction.x;                 //reverse x velocity
            }
            if (collision.GetContact(0).normal.y != 0.0f)   //hits wall
            {
                direction.y = -direction.y;                 //reverse y velocity
            }

        }
    }

    public IEnumerator ColCheckRefresh(float time)      //used in the collision check delay
    {
        yield return new WaitForSeconds(time);
        colCheck = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) //player is in range to chase
    {
        if(collision.gameObject.tag == "Player")
        {
            if(chaser)
            {
                chasing = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //player left range
    {
        if (collision.gameObject.tag == "Player")
        {
            if (chaser)
            {
                chasing = false;

                if(!bouncer)    //bouncers want to keep moving without a target, but other types want to stay still
                {
                    direction = new Vector2(0, 0);
                    rigidBody.velocity = new Vector2(0,0);
                }
            }
        }
    }

}
