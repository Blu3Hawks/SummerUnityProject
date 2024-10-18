using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadPlayerInfo : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI playerCollectibles;
    [SerializeField] private TextMeshProUGUI playerCompletedLevels;

    [Header("Playable Levels")]
    [SerializeField] private List<Button> levelButtons;

    private PlayerData playerData;
    private void Start()
    {
        if (PlayerProfile.Instance != null)
        {
            PlayerProfile.Instance.LoadPlayerData();
            playerData = PlayerProfile.Instance.currentPlayerData;
        }

        LoadPlayerInfoToText();
        LoadPlayerLevels();
    }


    private void LoadPlayerInfoToText()
    {
        playerName.text = "Name: " + playerData.playerName;
        playerScore.text = "Total Score: " + playerData.totalScore;
        playerCollectibles.text = "Total Collectibles: " + playerData.collectibleItems;
        playerCompletedLevels.text = "Current Level: " + playerData.currentLevel;
    
    }

    private void LoadPlayerLevels()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (i < playerData.currentLevel)
            {
                levelButtons[i].interactable = true;  // Enable unlocked levels
            }
            else
            {
                levelButtons[i].interactable = false;  // Disable locked levels
            }
        }
    }
}
