using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Transform startPoint, endPoint;
    private Transform tempTrans, resetTrans;
    public float speed;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    [Header("Move When Player Riding")]
    public bool detectPlayer;
    public float resetTime = 10f;
    private bool occupied;
    private bool shouldStillReset;
    private bool moving;
    private Transform originalStartPoint;
    private Transform originalEndPoint;

    void Start()
    {
        originalStartPoint = startPoint;    //track the original start and end point so they can reliably move back to the start
        originalEndPoint = endPoint;
        resetTrans = startPoint;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);


    }

    // Update is called once per frame
    void Update()
    {
        if (!detectPlayer || moving)    //this is here to stop player activated platforms from moving on game start
        {

            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;


            if (!detectPlayer || occupied)  //behave normally when player is riding, or if it doesn't care about players
            {
                // Set our position as a fraction of the distance between the markers. Smooth lerp looks more natural
                transform.position = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.SmoothStep(0, 1, fracJourney));

                if (transform.position == startPoint.position) Reset();
                if (transform.position == endPoint.position) Reset();
            }
            else    //if player activated & unoccupied then lerp from position player got off to end point
            {
                transform.position = Vector3.Lerp(resetTrans.position, endPoint.position, Mathf.SmoothStep(0, 1, fracJourney));

            }
        }


    }

    private void Reset()    //when the platform reaches the end point, swap start and end
    {
        tempTrans = startPoint;
        startPoint = endPoint;
        endPoint = tempTrans;

        startTime = Time.time;

        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
    }

    private void ResetToStart()     //reset to the original start point from current transform
    {
        startPoint = originalEndPoint;
        endPoint = originalStartPoint;
        resetTrans = transform;

        startTime = Time.time;

        journeyLength = Vector3.Distance(resetTrans.position, endPoint.position);
    }


    //set player (or enemies) resting on the platform to be its child so that they move with it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" /*|| collision.gameObject.tag == "Enemy"*/)
        {
            collision.transform.SetParent(this.gameObject.transform);

            if(detectPlayer)    //start moving if player onboard
            {
                occupied = true;
                shouldStillReset = false; //interrupt the delay for resetting the platform
                StopCoroutine(OnTriggerExit2D(collision));

                if (!moving)        //allow the platform to start moving
                {
                    moving = true;
                    startTime = Time.time;
                }
            }
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" /*|| collision.gameObject.tag == "Enemy"*/)
        {
            collision.transform.SetParent(null);

            if(detectPlayer)    //stop moving if player leaves
            {
                
                shouldStillReset = true;  //platform considered unoccupied 'resetTime' seconds after player gets off
                
                yield return new WaitForSeconds(resetTime); 

                if (shouldStillReset)
                {
                    ResetToStart();
                    occupied = false;
                }
            }
        }
    }

}
