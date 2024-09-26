using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    Animator anim;

    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;

    public float moveDistance = 5f;     //distance to move left or right
    public float moveSpeed = 10f;       //speed of movement
    public float jumpForce = 5f;        //force applied for the jump
    public bool isGrounded = true;      //track if player is grounded

    private Vector3 targetPosition;     //the target position for lateral movement
    private Rigidbody rb;               //reference to Rigidbody

    public float swipeThreshold = 50f;  //minimum swipe distance to be considered a valid swipe

    private void Start()
    {
        targetPosition = transform.position;  //initialize the player's target position to its current position at the start
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();  //ensure the Rigidbody is assigned
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) //check for touch input
        {
            swipeStartPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            swipeEndPos = Input.GetTouch(0).position;
            Vector2 swipeDelta = swipeEndPos - swipeStartPos;
                        
            if (swipeDelta.magnitude > swipeThreshold)  //check if the swipe exceeds the threshold
            {                
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))  //check horizontal swipe
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
                
                else if (swipeDelta.y > 0)  //check vertical swipe for jumping
                {
                    Jump();
                    Debug.Log("Swiped up");
                }
            }
        }

        
    }

    private void FixedUpdate()  //smoothly move horizontally in FixedUpdate
    {        
        Vector3 newPosition = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }
    public void MoveLeft() //move left
    {        
        targetPosition = new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z);
        if (isGrounded)
        {

            anim.SetTrigger("leftSwipe");
        }
    }

    public void MoveRight()   //move right
    {       
        targetPosition = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
        if (isGrounded)
        {

            anim.SetTrigger("rightSwipe");
        }
    }

    public void Jump()  //apply an upward force to the Rigidbody for jumping
    {       
        if (isGrounded)
        {
            isGrounded = false;  //prevent multiple jumps
            anim.SetBool("isGrounded", false);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("upSwipe");
        }
    }
        
    private void OnCollisionEnter(Collision collision)  //check if the player is grounded
    {
        if (collision.gameObject.CompareTag("Terrain"))  //make sure ground has the tag "Terrain"
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
            Debug.Log("Player is grounded");
        }
    }
}
