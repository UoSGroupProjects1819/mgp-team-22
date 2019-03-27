using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager_old : MonoBehaviour
{
    public bool isRight;
    public enum gravityDirection {up, down, left, right}
    public gravityDirection GravityDirection;

   // private Vector3 grav;
   // private Vector3 tempGrav;

    private Vector3 up, down, left, right;

    // Start is called before the first frame update
    void Start()
    {
        up = Vector3.up * 9.8f;
        down = Vector3.down * 9.8f;
        left = Vector3.left * 9.8f;
        right = Vector3.right * 9.8f;

        //grav = Physics2D.gravity;
       // tempGrav = grav;

      //  print(grav.ToString());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isRight)
            {
                switch (GravityDirection)
                {
                    case gravityDirection.up:
                        Physics2D.gravity = left;
                        GravityDirection = gravityDirection.left;
                        break;

                    case gravityDirection.right:
                        Physics2D.gravity = up;
                        GravityDirection = gravityDirection.up;
                        break;

                    case gravityDirection.down:
                        Physics2D.gravity = right;
                        GravityDirection = gravityDirection.right;
                        break;

                    case gravityDirection.left:
                        Physics2D.gravity = down;
                        GravityDirection = gravityDirection.down;
                        break;

                }
            }

            if (!isRight)
            {
                switch (GravityDirection)
                {
                    case gravityDirection.up:
                        Physics2D.gravity = left;
                        GravityDirection = gravityDirection.left;
                        break;

                    case gravityDirection.right:
                        Physics2D.gravity = down;
                        GravityDirection = gravityDirection.down;
                        break;

                    case gravityDirection.down:
                        Physics2D.gravity = left;
                        GravityDirection = gravityDirection.left;
                        break;

                    case gravityDirection.left:
                        Physics2D.gravity = up;
                        GravityDirection = gravityDirection.up;
                        break;

                }
            }
        }
    }
}


