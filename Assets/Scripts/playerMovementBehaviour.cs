using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovementBehaviour : MonoBehaviour
{
    [SerializeField] public InputAction playerControls;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 movementDirection;
    public float moveSpeed = 5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        // getting moveement inputs and sending to animator
        movementDirection = playerControls.ReadValue<Vector2>();
        Debug.Log(movementDirection);
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);

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
        //actual movement here
        //rb.velocity = new Vector2(movementDirection.x * moveSpeed * Time.fixedDeltaTime, movementDirection.y * moveSpeed * Time.fixedDeltaTime); 
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
