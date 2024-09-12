using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    Animator anim;

    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;

    public float moveDistance = 5f;     // Distance to move left or right
    public float moveSpeed = 10f;       // Speed of movement
    public float jumpForce = 5f;        // Force applied for the jump
    public bool isGrounded = true;      // Track if player is grounded

    private Vector3 targetPosition;     // The target position for lateral movement
    private Rigidbody rb;               // Reference to Rigidbody

    public float swipeThreshold = 50f;  // Minimum swipe distance to be considered a valid swipe

    private void Start()
    {
        // Initialize the player's target position to its current position at the start
        targetPosition = transform.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();  // Ensure the Rigidbody is assigned
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            swipeStartPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            swipeEndPos = Input.GetTouch(0).position;
            Vector2 swipeDelta = swipeEndPos - swipeStartPos;

            // Check if the swipe exceeds the threshold
            if (swipeDelta.magnitude > swipeThreshold)
            {
                // Check horizontal swipe
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x < 0)
                    {
                        MoveLeft();
                        Debug.Log("Swiped left");
                    }
                    else
                    {
                        MoveRight();
                        Debug.Log("Swiped right");
                    }
                }
                // Check vertical swipe for jumping
                else if (swipeDelta.y > 0)
                {
                    Jump();
                    Debug.Log("Swiped up");
                }
            }
        }

        
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
