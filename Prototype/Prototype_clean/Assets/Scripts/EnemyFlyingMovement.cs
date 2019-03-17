﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingMovement : MonoBehaviour
{

    Rigidbody2D rigidbody;

    public enum MovementType        //enum used for a nice dropdown in the editor
    {
        Bouncer,        //bounces when it hits a wall
        Chaser
    }
    public MovementType movementType;

    public bool bouncer;
    public bool chaser;

    public Vector2 destination;
    public Vector2 direction;
    public float moveSpeed;

    float updateTime;   //delay between destination updates when tracking player - we don't need to decide where we're going every frame

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        direction = new Vector2(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(gameObject.transform.localPosition, new Vector3(gameObject.transform.localPosition.x + direction.x,
            gameObject.transform.localPosition.y + direction.y, gameObject.transform.localPosition.z), Color.red);
    }

    private void FixedUpdate()
    {
        if(bouncer)
        {
            rigidbody.AddForce(direction);

            if (rigidbody.velocity.magnitude > moveSpeed)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * moveSpeed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Debug.Log(collision.GetContact(0).normal);
            if (collision.GetContact(0).normal.x != 0.0f)   //hits floor or ceiling
            {
                direction.x = -direction.x;
            }
            if (collision.GetContact(0).normal.y != 0.0f)   //hits wall
            {
                direction.y = -direction.y;
            }
            
        }
    }

    public IEnumerator MovingTargetUpdate()
    {
        yield return new WaitForSeconds(1f);
    }
}