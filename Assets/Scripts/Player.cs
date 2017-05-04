using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Setting")]
    public float maxSpeed = 10f; // Fastest the player can travel
    public float jumpForce = 400f; // Amount of force for jump
    [Range(0, 1)] public float crouchSpeed = 0.30f; // speed applied
    public bool airControl = false; // Allow steering while in air
    public LayerMask whatIsGround; // A layer mask to indicate ground
    

    private bool facingRight = true; // which way is player facing
    private Transform groundCheck;
    private float groundRadius = 0.2f;
    private bool grounded = false; // checking if we are grounded
    private Transform ceilingCheck;
    private float ceilingRadius = 0.1f;
    private Animator anim;
    private Rigidbody2D rigid;

    // Use this for initialization
    void Awake()
    {
        //set up all our references
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        

    }

    // FixedUpdate is called at a specific framerate
    void FixedUpdate()
    {
        // Performing ground check (using Physics2D)
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigid.velocity.y);

    }

    void Flip()
    {
        // Switch the way is player is facing
        facingRight = !facingRight;

        // Invert the scale of the player on X
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inverts X
        transform.localScale = scale;

    }

    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if we can stand up
        if(crouch == false)
        {
            // Check to see if we hit ceiling
            if(Physics2D.OverlapCircle(ceilingCheck.position,ceilingRadius,whatIsGround))
            {
                crouch = true;
            }
        }

        anim.SetBool("Crouch", crouch);

        // Only control player if grounded or airControl is on
        if(grounded || airControl)
        {
            // Reduce the speed if we are crouching
            move = (crouch ? move * crouchSpeed : move);

            anim.SetFloat("Speed",Mathf.Abs(move));

            // Move the character
            rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);


            // If the input is moving player right
            if(move > 0 && !facingRight)
            {
                Flip();
            }
            else if(move < 0 && facingRight)
            {
                Flip();
            }
        }

        // If i'm grounded and trying to jump
        if(grounded && jump && anim.GetBool("Ground"))
        {
            anim.SetBool("Ground", false);
            grounded = false;
            rigid.AddForce(new Vector2(0, jumpForce));
        }

    }


}
