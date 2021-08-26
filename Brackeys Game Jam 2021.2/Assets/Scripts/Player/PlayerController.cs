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
    private float lastTimeJumped;
    public float coyoteTime;
    private bool wasGrounded;
    public Vector2 velocity;
    
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
        if (Input.GetKeyDown(upKey) && ((isGrounded() || Time.realtimeSinceStartup-lastTimeJumped < coyoteTime)|| !doubleJumped)){
            
            doubleJumped = !isGrounded() && Time.realtimeSinceStartup-lastTimeJumped > coyoteTime; // Check if on ground or jumped because of coyote time, and set the ability to double jump appropriately
            
            velocity.y = jumpHeight; // Move player up
        }
        rb.velocity = velocity; // Set velocity
    }
}
