using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float movementThreshold = 0.3f;

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

    private void FixedUpdate()
    {
        CheckJoystickInput();
    }
    private void CheckJoystickInput()
    {
        Vector2 joystickDirection = joystick.Direction;

        // Check if joystick is moved in any direction
        if (joystickDirection.magnitude > movementThreshold)
        {
            // Check if Y is the dominant direction
            if (Mathf.Abs(joystickDirection.y) > Mathf.Abs(joystickDirection.x))
            {
                // Y movement is dominant (jump)
                if (joystickDirection.y > 0)
                {
                    Debug.Log("We moved on the positive y");
                    player.Jump();
                }
            }
            else
            {
                // X movement is dominant (left or right)
                if (joystickDirection.x < 0)
                {
                    player.MoveLeft();
                }
                else if (joystickDirection.x > 0)
                {
                    player.MoveRight();
                }
            }
        }
    }
}
