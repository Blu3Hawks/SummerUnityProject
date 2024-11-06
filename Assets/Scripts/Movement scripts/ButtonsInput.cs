using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsInput : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Vector3 offsetPositionHorizontal; // x = -85

    [SerializeField] private Vector3 offsetPositionVertical; // x = 600 y = -150
    private Camera mainCamera; 
    [SerializeField] private UI_HealthHandler uiHealthHandler;


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
        uiHealthHandler = FindObjectOfType<UI_HealthHandler>();
        mainCamera = uiHealthHandler.mainCamera;
    }

    private void Update()
    {
        PositionButtons();
    }

    public void PressButtonLeft()
    {
        if (!PauseMenu.gameIsPaused)
        {
            player.MoveLeft();
        }
    }

    public void PressButtonRight()
    {
        if (!PauseMenu.gameIsPaused)
        {
            player.MoveRight();
        }
    }

    public void PressButtonJump()
    {
        if (!PauseMenu.gameIsPaused)
        {
            player.Jump();
        }
    }

    public void PressButtonPowerUp()
    {
        if (!PauseMenu.gameIsPaused)
        {
            player.UsePowerUp();
        }
    }
    private void PositionButtons()
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
            transform.localScale = Vector3.one;
        }
        else
        {
            joystickPosition = new Vector3(
                bottomCenter.x + offsetPositionVertical.x,
                bottomCenter.y + offsetPositionVertical.y,
                bottomCenter.z - offsetPositionVertical.z
            );
            Debug.Log("We've turned non-portrait");
            transform.localScale = Vector3.one * 0.7f;
        }

        transform.position = joystickPosition;
        transform.localScale = Vector3.one * screenScaleFactor;
    }
}
