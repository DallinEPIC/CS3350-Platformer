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

    private bool isGrounded;
    private Vector2 movementInput;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;


    void Start()
    {
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
    // Update is called once per frame
    void Update()
    {
        // getting moveement inputs and sending to animator
        movementInput = PlayerControls.ReadValue<Vector2>();
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
        animator.SetBool("IsJumping", !isGrounded);
        //movement
        rb.velocity = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);

        //jumping
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        //flipping sprite
        if (animator.GetFloat("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    private Rigidbody2D rb;
    void FixedUpdate()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
}
