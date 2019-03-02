using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Transform startPoint, endPoint;
    private Transform tempTrans;
    public float speed;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;


    void Start()
    {
            // Keep a note of the time the movement started.
            startTime = Time.time;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {

        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers. Smooth lerp looks more natural
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.SmoothStep(0, 1, fracJourney));

        if (transform.position == startPoint.position) Reset();
        if (transform.position == endPoint.position) Reset();

    }

    private void Reset()
    {
        tempTrans = startPoint;
        startPoint = endPoint;
        endPoint = tempTrans;

        startTime = Time.time;

        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
    }


    //set player (or enemies) resting on the platform to be its child so that they move with it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            collision.transform.SetParent(this.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            collision.transform.SetParent(null);
        }
    }

}
