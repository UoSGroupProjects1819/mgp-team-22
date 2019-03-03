using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime);


    }

    //// private void OnTriggerEnter2D(Collider2D collision)
    // {
    //    // if (collision.gameObject.tag == "Ground")
    //     {

    //         //moveSpeed *= -1;

    //     }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground") moveSpeed = moveSpeed * -1;

        else if (collision.gameObject.tag == "Edge") moveSpeed = moveSpeed * -1;
    }



}
