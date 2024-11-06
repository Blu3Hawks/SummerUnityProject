using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player's stats")]
    public float moveDistance = 5f;     // Distance to move left or right
    public float moveSpeed = 10f;       // Speed of movement
    public float jumpForce = 5f;        // Force applied for the jump
    public bool isGrounded = true;      // Track if player is grounded
    [Header("Player's transforms")]
    private Vector3 targetPosition;     // The target position for lateral movement
    private Rigidbody rb;               // Reference to Rigidbody

    [Header("Player's buffs")]
    public float powerUpJumpForce;
    public float speedTimer;

    public int powerUpAddedScore;
    public int moveSpeedAddedScore;
    [SerializeField] private GameObject roadTile;
    [Range(1f, 4f)]
    [SerializeField] private float speedBuffScaler;

    [SerializeField] private UI_PowerUpHandler powerUpHandler;
    [SerializeField] private UI_HealthHandler healthHandler;
    [SerializeField] private UI_Score uiScore;
    private Animator anim;

    private void Start()
    {
        // Initialize the player's target position to its current position at the start
        targetPosition = transform.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();  // Ensure the Rigidbody is assigned
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        powerUpHandler = FindObjectOfType<UI_PowerUpHandler>();
        healthHandler = FindObjectOfType<UI_HealthHandler>();
        uiScore = FindObjectOfType<UI_Score>();
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



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))  // Make sure your ground has the tag "Ground"
        {
            isGrounded = true;
            Debug.Log("Player is grounded");
        }
        if (other.CompareTag("MoveSpeed"))
        {
            StartCoroutine(SpeedBoost());
            Destroy(other.gameObject);
        }
        if (other.CompareTag("PowerUp"))
        {
            powerUpHandler.AddPowerup();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {
            healthHandler.TakeDamage();
            Destroy(other.gameObject);
            Handheld.Vibrate();
        }
    }

    public void UsePowerUp()
    {
        if (powerUpHandler.powerUpObjects.Count > 0)
        {
            for (int i = 0; i < powerUpHandler.powerUpObjects.Count; i++)
            {
                GameObject lastPowerUp = powerUpHandler.powerUpObjects[powerUpHandler.powerUpObjects.Count - 1];
                powerUpHandler.powerUpObjects.RemoveAt(powerUpHandler.powerUpObjects.Count - 1);
                Destroy(lastPowerUp);
                powerUpHandler.currentPowerUps--;
                powerUpHandler.PositionObjects();
            }
            ApplyPowerUp();
        }
    }

    private void ApplyPowerUp()
    {
        rb.AddForce(Vector3.up * powerUpJumpForce, ForceMode.Impulse);
        uiScore.scoreAmount += powerUpAddedScore;
    }
    public IEnumerator SpeedBoost()
    {
        Time.timeScale *= speedBuffScaler;
        yield return new WaitForSeconds(speedTimer);
        Time.timeScale /= speedBuffScaler;
    }
}
