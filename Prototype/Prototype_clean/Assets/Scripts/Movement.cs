using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 15)]
    float maxJump;

    [SerializeField]
    [Range(8, 10)]
    float runSpeed;

    [SerializeField]
    [Range(0, 1)]
    float minJump = 0.5f;

    private float moveHorizontal, moveVertical;
    private Rigidbody2D rb2d;
    private PlayerController_Backup playerCon;
    public bool grounded;


    float JumpPressedRemember = 0;
    [SerializeField]
    float preJump = 0.2f;

    float GroundedRemember = 0;
    [SerializeField]
    float edgeJump = 0.25f;

    [SerializeField]
    [Range(0, 1)]
    float DampingMaster = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float StopDamping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float TurnDamping = 0.5f;

    [Header("Debug")]
    public bool flyMode;
    private float storeGravScale;





    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerCon = GetComponent<PlayerController_Backup>();
   
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && playerCon.firing == false)
        {
            grounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && playerCon.firing == false)
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
            if (rb2d.velocity.y > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * minJump);
            }
        }
    }


    void FixedUpdate()
    {
        
        if ((JumpPressedRemember > 0) && (GroundedRemember > 0))
        {
            JumpPressedRemember = 0;
            GroundedRemember = 0;
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxJump);
        grounded = false;
        }

        float fHorizontalVelocity = rb2d.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        //   fHorizontalVelocity = (fHorizontalVelocity * Time.deltaTime * 100);

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - StopDamping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
            fHorizontalVelocity *= Mathf.Pow(1f - TurnDamping, Time.deltaTime * 10f);
        else
            fHorizontalVelocity *= Mathf.Pow(1f - DampingMaster, Time.deltaTime * 10f);

        if (flyMode)
        {
            float fVerticalVelocity = rb2d.velocity.y;
            fVerticalVelocity += Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.01f)
                fVerticalVelocity *= Mathf.Pow(1f - StopDamping, Time.deltaTime * 10f);
            else if (Mathf.Sign(Input.GetAxisRaw("Vertical")) != Mathf.Sign(fVerticalVelocity))
                fVerticalVelocity *= Mathf.Pow(1f - TurnDamping, Time.deltaTime * 10f);
            else
                fVerticalVelocity *= Mathf.Pow(1f - DampingMaster, Time.deltaTime * 10f);


            rb2d.velocity = new Vector2((fHorizontalVelocity / 10) * runSpeed, (fVerticalVelocity / 10) * runSpeed);
        }
        else
        {
            rb2d.velocity = new Vector2((fHorizontalVelocity / 10) * runSpeed, rb2d.velocity.y);
        }
        
    }
    

    public void bounceMovement()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, maxJump);
    }

    //private void FixedUpdate()
    //{
    //    Vector2 move = new Vector2(moveHorizontal * runSpeed * 100 * Time.deltaTime, 0f);
    //    rigidBody.AddForce(move);
    //}

    public void ToggleFly()
    {
        flyMode = !flyMode;

        if (rb2d.gravityScale != 0)
        {
            storeGravScale = rb2d.gravityScale;
            rb2d.gravityScale = 0f;
        }
        else
        {
            rb2d.gravityScale = storeGravScale;
        }
    }

}
