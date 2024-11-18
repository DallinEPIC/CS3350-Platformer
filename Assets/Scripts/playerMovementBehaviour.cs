using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovementBehaviour : MonoBehaviour
{
    [SerializeField] public InputAction PlayerControls;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [HideInInspector] public static playerMovementBehaviour instance;
    [HideInInspector] public Rigidbody2D rb;

    private bool hasDoubleJump;
    private bool isGrounded;
    public bool isSliding;
    public Vector2 MovementInput;
    private Vector2 externalForce; // New variable for external forces
    public float moveSpeed = 5f; // Multiplier for speed
    public float jumpForce = 10f; // Multiplier for jumping
    public float fallMultiplier = 2.5f; // Gravity multiplier for falling
    public float lowJumpMultiplier = 2f; // Gravity multiplier for low-height jumps
    public float wallSlideMultiplier = 0.5f; // Gravity multiplier for sliding on walls
    public bool facingRight;
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }

    void Update()
    {
        // Getting movement inputs and sending to animator
        MovementInput = PlayerControls.ReadValue<Vector2>();
        animator.SetFloat("Horizontal", MovementInput.x);
        animator.SetFloat("Vertical", MovementInput.y);
        animator.SetFloat("Speed", MovementInput.sqrMagnitude);
        animator.SetBool("isJumping", !isGrounded);


        // Jumping

        // Check if the player is falling
        if (rb.velocity.y < 0 && !isSliding)
        {
            // Apply the fall multiplier to make falling faster
            rb.gravityScale = fallMultiplier;
        }
        // Check if the player is jumping but has released the jump button
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            // Apply the low jump multiplier to make jumps shorter when the button is released early
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            // Reset gravity to the default value
            rb.gravityScale = 1f;
        }
        //grounded check
        if (isGrounded)
        {
            hasDoubleJump = true;
        }
        //normal jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }
        //double jump
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isGrounded && hasDoubleJump && !isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            hasDoubleJump = false;
            animator.SetTrigger("DoubleJump");
        }

        // Flipping sprite
        if (animator.GetFloat("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
            facingRight = false;
        }
        else
        {
            spriteRenderer.flipX = false;
            facingRight = true;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        // Movement: Combine player input and external forces
        Vector2 movementForce = new Vector2(MovementInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = movementForce + externalForce;

        // Clamp velocity to prevent excessive speeds
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -moveSpeed, moveSpeed),
            Mathf.Clamp(rb.velocity.y, -jumpForce * 1.5f, jumpForce * 1.5f)
        );

        // Reduce external force gradually for smoother deceleration
        externalForce = Vector2.Lerp(externalForce, Vector2.zero, Time.deltaTime * 5f);

    }

    // New method to add external forces (e.g., from arrows)
    public void AddExternalForce(Vector2 force)
    {
        externalForce += force;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contactPoint in collision.contacts)
        {
            // Draw the normal vector for each contact point
            Vector3 contactPosition = contactPoint.point; // Get the contact point position
            Vector3 normalEnd = contactPosition + (Vector3)contactPoint.normal; // Calculate the end point of the normal
            Debug.DrawLine(contactPosition, normalEnd, Color.red, 2.5f); // Draw the normal as a red line


            if (contactPoint.collider.CompareTag("Ground")) {
                //ground checking
                if (Mathf.Abs(contactPoint.normal.y) > 0.5f)
                {
                    animator.SetBool("WallSlide", false);
                    isSliding = false;
                }
                //wall checking
                if (Mathf.Abs(contactPoint.normal.x) > 0.5f && !isGrounded)
                {
                    isSliding = true;
                    Debug.Log($"wall sliding: {isSliding}");
                    animator.SetBool("WallSlide", true);
                    rb.gravityScale = wallSlideMultiplier;
                    hasDoubleJump = true;
                    //wall jump
                    if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isGrounded && isSliding)
                    {
                        if(facingRight)
                        {
                            rb.velocity = new Vector2(1, jumpForce);
                        }
                        else
                        {
                            rb.velocity = new Vector2(-1, jumpForce);
                        }
                        
                        animator.SetTrigger("WallJump");
                    }

                }
            }
        }
    }
}
