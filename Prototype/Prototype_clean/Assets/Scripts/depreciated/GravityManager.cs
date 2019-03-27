using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{

    public enum gravityDirection {up, down, left, right}
    public gravityDirection GravityDirection;
    
    private Vector3 up, down, left, right;

    // Start is called before the first frame update
    void Start()
    {
        up = Vector3.up * 9.8f;
        down = Vector3.down * 9.8f;
        left = Vector3.left * 9.8f;
        right = Vector3.right * 9.8f;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                switch (GravityDirection)
                {
                    case gravityDirection.up:
                    Physics2D.gravity = up;
                    break;

                    case gravityDirection.right:
                    Physics2D.gravity = right;
                    break;

                    case gravityDirection.down:
                    Physics2D.gravity = down;
                    break;

                    case gravityDirection.left:
                    Physics2D.gravity = left;
                    break;

                }
            }
          
    }
}


