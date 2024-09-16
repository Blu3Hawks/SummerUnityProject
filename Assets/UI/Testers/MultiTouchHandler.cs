using UnityEngine;

public class MultiTouchHandler : MonoBehaviour
{
    [Header("Swipes")]
    [SerializeField] private float minSwipeLength = 60;
    [SerializeField] private float maxThreshDiff = 10;

    [Header("Pinching")]
    [SerializeField] private float minPinchDistance = 10f;
    [SerializeField] private float percentagePinchScaling;

    private Vector2[] startingPositions;

    private void Start()
    {
        startingPositions = new Vector2[4];
    }
    void Update()
    {
        DetectTouch();
        DetectPinch();
    }

    private void DetectPinch()
    {
        int touchCount = Input.touchCount;
        for (int i = 0; i < touchCount; i++)
        {
            if(touchCount ==2)
            {
                Touch touch = Input.GetTouch(i);

                // Display information about each touch
                //  Debug.Log("Touch " + i + " - Position: " + touch.position + " Phase: " + touch.phase);
                Vector2 startingPos = touch.position;
                //Get pinching info here
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                Vector2 touch0_startingPos = touch0.position;
                Vector2 touch1_startingPos = touch1.position;

                if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                {
                    Vector2 touch0_currentPos = touch.position;
                    Vector2 touch1_currentPos = touch1.position;

                    PinchDownScreen(touch0_startingPos, touch0_currentPos, touch1_startingPos, touch1_currentPos);
                }
            }
        }
    }

    private void DetectTouch()
    {
        // Check how many touches are currently on the screen
        int touchCount = Input.touchCount;

        if (touchCount < 1)
            return;

        // Loop through all touches
        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Display information about each touch
            //  Debug.Log("Touch " + i + " - Position: " + touch.position + " Phase: " + touch.phase);
            Vector2 startingPos = touch.position;

            // You can add additional touch handling logic here
            // For example, handle different touch phases
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Handle touch start
                    //  Debug.Log("Touch " + i + " started at position " + touch.position);
                    startingPositions[i] = touch.position;
                    break;
                case TouchPhase.Moved:
                    // Handle touch moved
                    //    Debug.Log("Touch " + i + " moved to position " + touch.position);
                    Vector2 endPos = touch.position;
                    CalculateSwipes(startingPos, endPos);
                    break;
                case TouchPhase.Stationary:
                    // Handle touch stationary
                    //    Debug.Log("Touch " + i + " is stationary at position " + touch.position);
                    break;
                case TouchPhase.Ended:
                    // Handle touch end
                    //   Debug.Log("Touch " + i + " ended at position " + touch.position);
                    endPos = touch.position;
                    CalculateSwipes(startingPositions[i], endPos);
                    break;
                case TouchPhase.Canceled:
                    // Handle touch canceled
                    //  Debug.Log("Touch " + i + " was canceled at position " + touch.position);
                    break;
            }
        }
    }
    private void CalculateSwipes(Vector2 startingPos, Vector2 endPos)
    {
        Vector2 calculatedSwipePos = startingPos - endPos;

        if (calculatedSwipePos.x > minSwipeLength && Mathf.Abs(calculatedSwipePos.y) < maxThreshDiff)
        {
            Debug.Log("We have swiped left!");
        }
        else if (calculatedSwipePos.x < -minSwipeLength && Mathf.Abs(calculatedSwipePos.y) < maxThreshDiff)
        {
            Debug.Log("We swiped right!");
        }
        else if (calculatedSwipePos.y < -minSwipeLength && Mathf.Abs(calculatedSwipePos.x) < maxThreshDiff)
        {
            Debug.Log("We swiped up");
        }
        else if (calculatedSwipePos.y > minSwipeLength && Mathf.Abs(calculatedSwipePos.x) < maxThreshDiff)
        {
            Debug.Log("We swiped down");
        }
        else
        {
            return;
        }
    }

    private void PinchDownScreen(Vector2 startingPos_0, Vector2 startingPos_1, Vector2 endPos_0, Vector2 endPos_1)
    {
        int touchCount = Input.touchCount;

        if (touchCount > 1)
        {
            float startingPosDistance = Mathf.Abs(startingPos_0.x - startingPos_1.x) + Mathf.Abs(startingPos_0.y - startingPos_1.y);

            if (startingPosDistance > minPinchDistance) //if 12 > 10 then continue IE
            {
                float endPosDistance = Mathf.Abs(endPos_0.x - endPos_1.x) + Mathf.Abs(endPos_0.y - endPos_1.y); //calculate end positions, say it's 3
                float scalingPinch = endPosDistance / startingPosDistance; // say 12/ 3 = 4.
                                                                           //now we can do some calculations and stuff with the percentagePinchScaling but not just quite yet.
                Debug.Log("We pinched a bit");
            }
            else
            {
                return;
            }
        }
    }
}
