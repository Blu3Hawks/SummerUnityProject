using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float movementThreshold = 0.3f;
    private ScreenOrientation currentOrientation;
    [SerializeField] private UI_HealthHandler uiHealthHandler;
    private Camera mainCamera;

    [SerializeField] private Vector3 offsetPositionHorizontal; // x = 570 y = 400
    [SerializeField] private Vector3 offsetPositionVertical; // x = 1250 y = 200
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
        PositionJoystick();
        currentOrientation = Screen.orientation;
        uiHealthHandler = FindObjectOfType<UI_HealthHandler>();
        mainCamera = uiHealthHandler.mainCamera;
    }

    private void FixedUpdate()
    {
        CheckJoystickInput();
        PositionJoystick();
    }
    private void CheckJoystickInput()
    {
        Vector2 joystickDirection = joystick.Direction;

        // Check if joystick is moved in any direction
        if (joystickDirection.magnitude > movementThreshold)
        {
            if (Mathf.Abs(joystickDirection.y) > Mathf.Abs(joystickDirection.x))
            {
                if (joystickDirection.y > 0)
                {
                    Debug.Log("We moved on the positive y");
                    player.Jump();
                }
                else if(joystickDirection.y < 0)
                {
                    Debug.Log("We jumped down - or used powerup");
                    player.UsePowerUp();
                }
            }
            else
            {
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

    private void PositionJoystick()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera is not assigned.");
            return;
        }

        Vector3 bottomCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane + 5f));

        bool isPortrait = Screen.height > Screen.width;

        float screenScaleFactor = Mathf.Min(Screen.width, Screen.height) * 0.001f; 

        Vector3 joystickPosition;
        if (isPortrait)
        {
            joystickPosition = new Vector3(
                bottomCenter.x + offsetPositionHorizontal.x,
                bottomCenter.y + offsetPositionHorizontal.y,
                bottomCenter.z - offsetPositionHorizontal.z
            );
            Debug.Log("We've turned portrait");
        }
        else
        {
            joystickPosition = new Vector3(
                bottomCenter.x + offsetPositionVertical.x,
                bottomCenter.y + offsetPositionVertical.y,
                bottomCenter.z - offsetPositionVertical.z
            );
            Debug.Log("We've turned non-portrait");
        }

        joystick.transform.position = joystickPosition;
        joystick.transform.localScale = Vector3.one * screenScaleFactor; 
    }
}
