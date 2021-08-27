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
    
    // Get references
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    
    bool isGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return ray;
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
        }
        rb.velocity = velocity; // Set velocity
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Candy" && Input.GetKeyDown(KeyCode.F))
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
}
