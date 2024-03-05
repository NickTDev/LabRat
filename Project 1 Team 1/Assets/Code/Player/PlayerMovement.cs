using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Ingredient defaultIngredient;
    [HideInInspector] public Ingredient heldIngredient;

    [Header("Movement")]
    float moveInput = 0f;
    //[Tooltip("The speed of the player")]
    //public float speed = 5f;
    //[Tooltip("Hw fast the player speeds up")]
    //public float acceleration = 1f;
    //[Tooltip("How fast the player slows down after the player realeases the movement key")]
    //public float decceleration = 1f;
    //[Tooltip("How fast the player moves whhile crouching")]
    //public float crouchSpeed = 3f;
    public AudioSource jumpAudio;
    public AudioSource ingredientAudio;
    private float velocityPower = 1f;

    public float downGravityScale = 3;
    [Range(0f, 1f)] public float crouchSpeedFactor = 0.5f;
    //public float jumpHeight = 8f;
    //public int jumps;
    private int jumped;
    bool jumpInputDown = false;
    bool jumpInput = false;
    bool isGrounded = false;
    bool groundAbove = false;

    bool isCrouch = false;
    public bool IsCrouch { get => isCrouch; }

    Rigidbody2D rb;
    Animator anim;
    public LayerMask ground;
    public Collider2D groundCheckCollider;
    public Collider2D ceilingCheckCollider;
    public Collider2D standingCollider;
    public Collider2D crouchCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = transform.GetChild(4).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        jump();

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        if (heldIngredient == null)
        {
            DropIngredient();
        }

        CheckGround();
        movement();
        crouch();
    }

    public void DropIngredient()
    {
        heldIngredient = defaultIngredient;
        defaultIngredient.isHeld = false; // Hack to make this work 
    }

    //check for player inputs to see how the player should move
    void CheckInput()
    { 
        //Calculate the Left and Right Movment
        moveInput = Input.GetAxisRaw("Horizontal");

        //check if the player is trying to crouch
        bool crouchInput = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        if (crouchInput)
        {
            isCrouch = true;
        }
        else if (!groundAbove) // Only stop crouching when there is no ground above 
        {
            isCrouch = false;
        }

        if (isCrouch)
        {
            DropIngredient();
        }

        jumpInputDown = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        jumpInput = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }

    //move the player based on input
    void movement()
    {

        // get the input of the player to move left or right
        float targetSpeed = moveInput * heldIngredient.moveSpeed;

        if (isCrouch)
        {
            targetSpeed *= crouchSpeedFactor;
        }

        float speedDifference = targetSpeed - rb.velocity.x;

        //how fast the player is accelerating
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? heldIngredient.acceleration : heldIngredient.decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, velocityPower) * Mathf.Sign(speedDifference);

        rb.AddForce(movement * Vector2.right);
    }

    //allows the player to jump if they are on the ground
    void jump()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && jumped < heldIngredient.jumps && !isCrouch)
        //{
        //    rb.velocity = rb.velocity * Vector2.right; // Removes vertical velocity before jumping 
        //    rb.AddForce(Vector2.up * (Mathf.Sqrt(-2.0f * Physics2D.gravity.y * heldIngredient.jumpHeight) / rb.mass), ForceMode2D.Impulse);
        //    jumped++;
        //}

        //Check if the player is trying to jump or not
        if (jumpInputDown && isGrounded && isCrouch == false)
        {
            jumpAudio.Play();
            rb.velocity = rb.velocity * Vector2.right; // Removes vertical velocity before jumping 
            rb.AddForce(Vector2.up * (Mathf.Sqrt(-2.0f * Physics2D.gravity.y * heldIngredient.jumpHeight) / rb.mass), ForceMode2D.Impulse);
            jumped++;
        }
        else if (jumpInputDown && jumped < heldIngredient.jumps - 1 && isCrouch == false)
        {
            jumpAudio.Play();
            rb.velocity = rb.velocity * Vector2.right; // Removes vertical velocity before jumping 
            rb.AddForce(Vector2.up * (Mathf.Sqrt(-2.0f * Physics2D.gravity.y * heldIngredient.jumpHeight) / rb.mass), ForceMode2D.Impulse);
            jumped++;
        }

        // If going up and pressing space 
        // or if we are grounded (otherwise the increased gravity scale slows the player on the ground) 
        float gravityScale = heldIngredient == null ? 1 : heldIngredient.gravityScale; // Hot hix for null exeption 

        if (rb.velocity.y > 0 && jumpInput || isGrounded)
        {
            rb.gravityScale = gravityScale;
        }
        else
        {
            rb.gravityScale = gravityScale * downGravityScale;
        }

        ////if the player releases a button while they are jumping, stop the jump
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    if (rb.velocity.y > 0 && isGrounded == false)
        //    {
        //        rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        //    }
        //}
    }

    //check if the player is touching a platform
    void CheckGround()
    {
        isGrounded = groundCheckCollider.IsTouchingLayers(ground.value);

        groundAbove = ceilingCheckCollider.IsTouchingLayers(ground.value);

        if (isGrounded && !Input.GetKey(KeyCode.Space)) // Input is needed bc otherwise jumped gets instantly set to 0 after jumping 
        {
            jumped = 0;
        }
    }

    //Allows the player to crouch
    void crouch()
    {
        if(isCrouch == true)
        {
            standingCollider.enabled = false;
        }
        else if(isCrouch == false && groundAbove == false)
        {
            standingCollider.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            //Checks if the player currently has a held item
            if (!heldIngredient.isHeld)
            {
                ingredientAudio.Play();
                heldIngredient.isHeld = true;

                IngredientManager otherObject;

                otherObject = other.GetComponent<IngredientManager>();
                heldIngredient = otherObject.ingredientValues;
            }
        }
    }

    void UpdateAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("groundAbove", groundAbove);
        anim.SetBool("isCrouching", isCrouch);
        anim.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("yVelocity", rb.velocity.y);
    }
}
