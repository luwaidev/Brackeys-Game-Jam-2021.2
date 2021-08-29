using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the player object

public class PlayerController : MonoBehaviour
{
    [Header("Key Bindings")]
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;

    [Header("References")]
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private Animator anim;

    [Header("Movement Variables")]
    public bool movementLocked;
    public float speed;
    public float jumpHeight;
    public bool doubleJumped;
    public Vector2 velocity;

    [Header("Coyote Time variables")]
    private float lastTimeJumped;
    private float lastJumpInput;
    public float coyoteTime;
    private bool wasGrounded;

    [Header("Candy Inventory")]
    public bool hasBlueCandy;
    public bool hasRedCandy;
    public bool hasGreenCandy;
    public SpriteRenderer redCandy;
    public SpriteRenderer blueCandy;
    public SpriteRenderer greenCandy;
    public Sprite[] sprites;
    
    // Get references
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Movement();
        Animations();
        ChangeCandyUI();
    }
    
    bool isGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return ray;
    }

    void Animations()
    {
        if (Input.GetKey(leftKey) || Input.GetKey(rightKey)) anim.SetBool("isRun", true);
        else anim.SetBool("isRun", false);

        anim.SetBool("isGrounded", isGrounded());
        
        if (Input.GetKey(rightKey)) transform.localScale = new Vector2(1,1);
        else if (Input.GetKey(leftKey)) transform.localScale = new Vector2(-1,1);;
    }

    void Movement()
    {
        velocity = rb.velocity;

        if (Input.GetKey(rightKey) && Input.GetKey(leftKey)) velocity.x = 0; // Don't do anything if both inputs are held down
        else if (Input.GetKey(rightKey)) velocity.x = speed; // Move right
        else if (Input.GetKey(leftKey)) velocity.x = -speed; // Move left
        else velocity.x = 0;

        // Check if just off ground
        if (wasGrounded != isGrounded()){
            lastTimeJumped = Time.realtimeSinceStartup;
        }
        wasGrounded = isGrounded();

        if (Input.GetKeyDown(upKey))
        {
            lastJumpInput = Time.realtimeSinceStartup;
        } 
        // If inputted the jump key, and is on the ground, left the ground no longer than coyote time, or is grounded and pressed jump while in the air
        if ((Input.GetKeyDown(upKey) || Time.realtimeSinceStartup-lastJumpInput < coyoteTime && isGrounded()) && ((isGrounded() || Time.realtimeSinceStartup-lastTimeJumped < coyoteTime )|| !doubleJumped)){
            
            doubleJumped = !isGrounded() && Time.realtimeSinceStartup-lastTimeJumped > coyoteTime; // Check if on ground or jumped because of coyote time, and set the ability to double jump appropriately
            
            velocity.y = jumpHeight; // Move player up

            if (!doubleJumped)anim.SetTrigger("Jump"); // Set jump animation
            else anim.SetTrigger("DoubleJump");
        }
        rb.velocity = velocity; // Set velocity
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Candy" && Input.GetKey(KeyCode.F))
        {
            if (other.name == "Blue")
            {
                hasBlueCandy = true;
            }
            else if (other.name == "Red")
            {
                hasRedCandy = true;
            }
            else if (other.name == "Green")
            {
                hasGreenCandy = true;
            }
        }
    }

    void ChangeCandyUI()
    {
        if (!hasRedCandy) redCandy.sprite = sprites[0];
        else redCandy.sprite = sprites[1];

        if (!hasBlueCandy) blueCandy.sprite = sprites[2];
        else blueCandy.sprite = sprites[3];

        if (!hasGreenCandy) greenCandy.sprite = sprites[4];
        else greenCandy.sprite = sprites[5];
    }
}
