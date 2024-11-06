using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    [SerializeField] private Player player;

    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;

    public float swipeThreshold = 50f;  // Minimum swipe distance to be considered a valid swipe


    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
            if (player == null)
            {
                Debug.LogError("The player is missing!");
            }
        }
    }

    private void Update()
    {
        // Check for touch input
       CheckTouchInput();
    }

    private void CheckTouchInput()
    {
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
                        player.MoveLeft();
                        Debug.Log("Swiped left");
                    }
                    else
                    {
                        player.MoveRight();
                        Debug.Log("Swiped right");
                    }
                }
                // Check vertical swipe for jumping
                else if (swipeDelta.y > 0)
                {
                    player.Jump();
                    Debug.Log("Swiped up");
                }
                else if(swipeDelta.y < 0)
                {
                    player.UsePowerUp();
                    Debug.Log("Swiped down");
                }
            }
        }
    }
}
