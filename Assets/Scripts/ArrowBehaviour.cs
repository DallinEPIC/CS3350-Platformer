using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float pushForce;
    [SerializeField] private CardinalDirection arrowDirection;
    private playerMovementBehaviour player;

    public enum CardinalDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    private void Update()
    {
        switch (arrowDirection)
        {
            case CardinalDirection.Up:
                transform.eulerAngles = new Vector3(0, 0, 0); break;
            case CardinalDirection.Left:
                transform.eulerAngles = new Vector3(0, 0, 90); break;
            case CardinalDirection.Right:
                transform.eulerAngles = new Vector3(0, 0, -90); break;
            case CardinalDirection.Down:
                transform.eulerAngles = new Vector3(0, 0, 180); break;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        player = playerMovementBehaviour.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure only the player triggers this
        {
            animator.SetBool("isTouching", true);
            //Debug.Log("touching");

            // Convert arrow direction to Vector2 and apply as external force
            Vector2 pushDirection = arrowDirection.ToVector2() * pushForce;
            player.AddExternalForce(pushDirection);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset animation when the player exits the arrow area
            animator.SetBool("isTouching", false);
        }
    }
}

// Extension class for CardinalDirection
public static class CardinalDirectionExtensions
{
    public static Vector2 ToVector2(this ArrowBehaviour.CardinalDirection direction)
    {
        if (direction == ArrowBehaviour.CardinalDirection.Up) return Vector2.up;
        if (direction == ArrowBehaviour.CardinalDirection.Down) return Vector2.down;
        if (direction == ArrowBehaviour.CardinalDirection.Left) return Vector2.left;
        if (direction == ArrowBehaviour.CardinalDirection.Right) return Vector2.right;
        return Vector2.zero;
    }
}