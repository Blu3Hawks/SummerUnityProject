using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveDistance = 5f;     // Distance to move left or right
    public float moveSpeed = 10f;       // Speed of movement
    public float jumpForce = 5f;        // Force applied for the jump
    public bool isGrounded = true;      // Track if player is grounded


    private Vector3 targetPosition;     // The target position for lateral movement
    private Rigidbody rb;               // Reference to Rigidbody

    Animator anim;

    private void Start()
    {
        // Initialize the player's target position to its current position at the start
        targetPosition = transform.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();  // Ensure the Rigidbody is assigned
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void FixedUpdate()
    {
        // Smoothly move horizontally in FixedUpdate
        Vector3 newPosition = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    public void MoveLeft()
    {
        // Move left
        targetPosition = new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z);
        if (isGrounded)
        {
            anim.SetTrigger("leftSwipe");
        }
    }

    public void MoveRight()
    {
        // Move right
        targetPosition = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
        if (isGrounded)
        {
            anim.SetTrigger("rightSwipe");
        }
    }

    public void Jump()
    {
        // Apply an upward force to the Rigidbody for jumping
        if (isGrounded)
        {
            isGrounded = false;  // Prevent multiple jumps
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("upSwipe");
        }
    }

    // Check if the player is grounded
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))  // Make sure your ground has the tag "Trigger"
        {
            isGrounded = true;
            Debug.Log("Player is grounded");
        }
    }
}
