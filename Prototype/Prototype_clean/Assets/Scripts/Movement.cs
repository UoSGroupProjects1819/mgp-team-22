using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float runSpeed, jumpHeight;
    private float moveHorizontal, moveVertical;
    private Rigidbody2D rigidBody;
    


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");


    }

    private void FixedUpdate()
    {
        Vector2 move = new Vector2(moveHorizontal * runSpeed * 100 * Time.deltaTime, 0f);
        rigidBody.AddForce(move);
    }


}
