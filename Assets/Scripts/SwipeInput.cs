using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;

    public float moveDistance = 5f;    // Distance to move left or right
    public float moveSpeed = 10f;      // Speed of movement

    private Vector3 targetPosition;    // The target position for the player to move to

    private void Start()
    {
        // Initialize the player's target position to its current position at the start
        targetPosition = transform.position;
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

            if (swipeEndPos.x < swipeStartPos.x)
            {
                MoveLeft();
            }
            else if (swipeEndPos.x > swipeStartPos.x)
            {
                MoveRight();
            }
        }

        // Smoothly move the player towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void MoveLeft()
    {
        // Move the target position to the left by the moveDistance
        targetPosition = new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z);
    }

    public void MoveRight()
    {
        // Move the target position to the right by the moveDistance
        targetPosition = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
    }
}
