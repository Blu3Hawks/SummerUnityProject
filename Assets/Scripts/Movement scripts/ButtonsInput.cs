using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsInput : MonoBehaviour
{
    [SerializeField] private Player player;

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

    public void PressButtonUp()
    {
        if (!PauseMenu.gameIsPaused)
        {
            player.Jump();
        }
    }
}
