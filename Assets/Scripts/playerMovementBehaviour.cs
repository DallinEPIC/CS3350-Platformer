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
    public static playerMovementBehaviour instance;
    public Rigidbody2D rb;

    private bool isGrounded;
    private Vector2 movementInput;
    private Vector2 externalForce; // New variable for external forces
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

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
        movementInput = PlayerControls.ReadValue<Vector2>();
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
        animator.SetBool("IsJumping", !isGrounded);

        // Movement: Combine player input and external forces
        Vector2 movementForce = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = movementForce + externalForce;

        // Clamp velocity to prevent excessive speeds
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -moveSpeed, moveSpeed),
            Mathf.Clamp(rb.velocity.y, -jumpForce * 1.5f, jumpForce * 1.5f)
        );

        // Reduce external force gradually for smoother deceleration
        externalForce = Vector2.Lerp(externalForce, Vector2.zero, Time.deltaTime * 5f);

        // Jumping
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Flipping sprite
        if (animator.GetFloat("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // New method to add external forces (e.g., from arrows)
    public void AddExternalForce(Vector2 force)
    {
        externalForce += force;
    }
}
