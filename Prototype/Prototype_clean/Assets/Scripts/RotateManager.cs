using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    public enum gravityDirection { up, down, left, right }
    public gravityDirection GravityDirection = gravityDirection.down;

    private gravityDirection saveGrav = gravityDirection.down;

    public bool flipFlop;
    public bool inverted;
    public bool requireGrounded;
    [Header("1,2,3,5,9,10")]
    public int rotationSpeed = 3;
    public float cooldown;
    bool onCooldown;
    

    public GameObject thingsToRotate;

    public GameObject player;

    private int rotTarget, rotCurrent;

    //public Vector3 upRot, leftRot, downRot, rightRot;

    // Start is called before the first frame update


    private void OnAwake()
    {
       // onCooldown = false;
      //  Rotate();
    }

    private void FixedUpdate()
    {



        if (rotTarget != rotCurrent)
        {
            if (rotTarget > rotCurrent)
            {
                thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), rotationSpeed);
                rotCurrent += rotationSpeed;
            }

            if (rotTarget < rotCurrent)
            {
                thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), -rotationSpeed);
                rotCurrent -= rotationSpeed;
            }
        }

        else if (rotTarget == rotCurrent)
        {
            rotCurrent = 0;
            rotTarget = 0;
        }

    }

    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    public void RotateNow()
    {
        switch (GravityDirection)
        {
            case gravityDirection.up:
                rotTarget = 180;
                RotateSaver.gravSave = RotateSaver.GravitySave.up;
                //thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1) , 180);
                break;

            case gravityDirection.right:
                rotTarget = -90;
                RotateSaver.gravSave = RotateSaver.GravitySave.right;
                //thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), -90);
                break;

            case gravityDirection.down:
                RotateSaver.gravSave = RotateSaver.GravitySave.down;
                rotTarget = 0;
                //thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), 0);
                break;

            case gravityDirection.left:
                rotTarget = 90;
                RotateSaver.gravSave = RotateSaver.GravitySave.left;
                //thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), 90);
                break;
        }
    }
    
    public void Rotate()
    {
        if (!onCooldown)
        {
            onCooldown = true;
            StartCoroutine(CoolDown());

            if (!requireGrounded || player.GetComponent<Movement>().grounded)
            {
                if (!flipFlop)
                {

                    RotateNow();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rotate();
        }
    }

    public void SaveGrav()
    {
        saveGrav = GravityDirection;
    }

    public void LoadGrav()
    {
        GravityDirection = saveGrav;
    }

    public void SetGrav()
    {
        switch (GravityDirection)
        {
            case gravityDirection.up:
                thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1) , 180);
                break;

            case gravityDirection.right:
                thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), -90);
                break;

            case gravityDirection.down:
                thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), 0);
                break;

            case gravityDirection.left:
                thingsToRotate.transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), 90);
                break;
        }
    }

}



