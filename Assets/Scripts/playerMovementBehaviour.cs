using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovementBehaviour : MonoBehaviour
{
    [SerializeField] public InputAction PlayerControls;

    [SerializeField] public Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;
    [HideInInspector] public static playerMovementBehaviour instance;
    [HideInInspector] public Rigidbody2D rb;
    public Transform endpoint;

    private AudioSource audioSource;
    private bool hasDoubleJump;
    private bool isGrounded;
    private bool isSliding;
    public Vector2 MovementInput;
    private Vector2 externalForce; // New variable for external forces
    public float moveSpeed = 5f; // Multiplier for speed
    public float jumpForce = 10f; // Multiplier for jumping
    public float fallMultiplier = 2.5f; // Gravity multiplier for falling
    public float lowJumpMultiplier = 2f; // Gravity multiplier for low-height jumps
    public float wallSlideMultiplier = 0.5f; // Gravity multiplier for sliding on walls
    [HideInInspector] public bool facingRight;
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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
        if(isSliding == false)
        {
            animator.SetBool("WallSlide", false);
        }
        //normal jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            JumpNoise();
        }
        //double jump
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isGrounded && hasDoubleJump && !isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            hasDoubleJump = false;
            animator.SetTrigger("DoubleJump");
            JumpNoise();
        }
        //wallJump
        else if (isSliding)
        {
            Debug.Log($"wall sliding: {isSliding}");
            animator.SetBool("WallSlide", true);
            rb.gravityScale = wallSlideMultiplier;
            hasDoubleJump = true;
            //wall jump
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isGrounded && isSliding)
            {
                AddExternalForce(new Vector2(-MovementInput.x * 10f, jumpForce));
                animator.SetTrigger("WallJump");
                JumpNoise();
            }
        }

        //CheatCode;
        if (Input.GetKeyDown(KeyCode.V))
            transform.position = endpoint.position;


        // Flipping sprite
        if (animator.GetFloat("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
            facingRight = false;
            wallCheck.position = transform.position - new Vector3(0.4f, 0);
        }
        else
        {
            spriteRenderer.flipX = false;
            facingRight = true;
            wallCheck.position = transform.position + new Vector3(0.4f, 0);
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        Debug.Log($"grounded = {isGrounded}");
        isSliding = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);
        
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
    public void JumpNoise()
    {
        audioSource.Play();
    }
    
}
