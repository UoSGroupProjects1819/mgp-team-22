using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingMovement : MonoBehaviour
{

    Rigidbody2D rigidBody;

    public enum MovementType        //enum used for a nice dropdown in the editor
    {
        Bouncer,        //bounces when it hits a wall
        Chaser
    }
    public MovementType movementType;

    public bool bouncer;
    public bool chaser;

    public bool chasing;

    public Vector2 destination;
    public Vector2 direction;
    public float moveAcceleration;
    public float moveSpeed;

    public GameObject player;

    float updateTime;   //delay between destination updates when tracking player - we don't need to decide where we're going every frame

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        direction = new Vector2(1, 1);
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
            if (transform.position.x < player.transform.position.x)
            {
                direction.x = 1;
            }
            else
            {
                direction.x = -1;
            }
            if (transform.position.y < player.transform.position.y)
            {
                direction.y = 1;
            }
            else
            {
                direction.y = -1;
            }
        }

        rigidBody.AddForce(direction*moveAcceleration);

        if (rigidBody.velocity.magnitude > moveSpeed)       //clamps move speed to a maximum
        {
            rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
        }

    }

    public bool colCheck = true;    //variable is true on creation because i'm trying to let it run once then prevent it

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && colCheck)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            

            if(chaser)
            {
                chasing = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (chaser)
            {
                chasing = false;
            }
        }
    }

    public IEnumerator MovingTargetUpdate()
    {
        yield return new WaitForSeconds(1f);
    }
}
