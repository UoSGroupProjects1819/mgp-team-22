using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    float maxJump;

    [SerializeField]
    [Range(0, 1)]
    float minJump = 0.5f;

    private float moveHorizontal, moveVertical;
    private Rigidbody2D rigidBody;
    private bool grounded;


    float JumpPressedRemember = 0;
    [SerializeField]
    float preJump = 0.2f;

    float GroundedRemember = 0;
    [SerializeField]
    float edgeJump = 0.25f;

    [SerializeField]
    float HorizontalAcceleration = 1;
    [SerializeField]
    [Range(0, 1)]
    float DampingMaster = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float StopDamping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float TurnDamping = 0.5f;





    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        grounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 position = (Vector2)transform.position + new Vector2(0, -0.7f);
        //Vector2 scale = (Vector2)transform.localScale + new Vector2(-0.02f, 0);

        //RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 0.8f);

        //if (hit.collider == null)
        //{
        //    print("nofloor");
        //    grounded = false;
        //}

        //else if (hit.collider.gameObject.tag == "Ground")
        //{
        //    print(hit.collider.gameObject.tag.ToString());
        //    grounded = true;
        //}



        //bool grounded = Physics2D.OverlapBox(position, scale, 0);

        GroundedRemember -= Time.deltaTime;
        if (grounded)
        {
            GroundedRemember = edgeJump;
        }

        JumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            JumpPressedRemember = preJump;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rigidBody.velocity.y > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * minJump);
            }
        }

        if ((JumpPressedRemember > 0) && (GroundedRemember > 0))
        {
            JumpPressedRemember = 0;
            GroundedRemember = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxJump);
        }

        float fHorizontalVelocity = rigidBody.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - StopDamping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
            fHorizontalVelocity *= Mathf.Pow(1f - TurnDamping, Time.deltaTime * 10f);
        else
            fHorizontalVelocity *= Mathf.Pow(1f - DampingMaster, Time.deltaTime * 10f);

        rigidBody.velocity = new Vector2(fHorizontalVelocity, rigidBody.velocity.y);
    }

    //private void FixedUpdate()
    //{
    //    Vector2 move = new Vector2(moveHorizontal * runSpeed * 100 * Time.deltaTime, 0f);
    //    rigidBody.AddForce(move);
    //}


}
