using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public PauseMenu.Difficulty diffculty;
    public float volume;
    public int inputSystem;

    public PlayerData(PauseMenu playerSettings)
    {
        diffculty = playerSettings.difficulty;
        volume = playerSettings.volume;
        inputSystem = playerSettings.systemIndex;
    }
}
