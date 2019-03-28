using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    public enum gravityDirection { up, down, left, right }
    public gravityDirection GravityDirection;
    public bool flipFlop;
    public bool inverted;

    public GameObject thingsToRotate;

    public GameObject player;

    private int rotTarget, rotCurrent;

    //public Vector3 upRot, leftRot, downRot, rightRot;

    // Start is called before the first frame update
    void Start()
    {


    }

    private void FixedUpdate()
    {



        if (rotTarget != rotCurrent)
        {
            if (rotTarget > rotCurrent)
            {
                thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), 3);
                rotCurrent += 3;
            }

            if (rotTarget < rotCurrent)
            {
                thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), -3);
                rotCurrent -= 3;
            }
        }

        else if (rotTarget == rotCurrent)
        {
            rotCurrent = 0;
            rotTarget = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!flipFlop)
            {
                switch (GravityDirection)
                {
                    case gravityDirection.up:
                        rotTarget = 180;
                        //thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1) , 180);
                        break;

                    case gravityDirection.right:
                        rotTarget = -90;
                        //thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), -90);
                        break;

                    case gravityDirection.down:
                        //thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), 0);
                        break;

                    case gravityDirection.left:
                        rotTarget = 90;
                        //thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), 90);
                        break;
                }
            }

            if (flipFlop)
            {
                float horiz = Input.GetAxisRaw("Horizontal");

                if (horiz > 0)
                {
                    if (!inverted)
                    {
                        rotTarget = -90;
                    }
                    else
                    {
                        rotTarget = 90;
                    }
                }

                if (horiz < 0)
                {
                    if (!inverted)
                    {
                        rotTarget = 90;
                    }
                    else
                    {
                        rotTarget = -90;
                    }

                }

            }

        }
    }
}



