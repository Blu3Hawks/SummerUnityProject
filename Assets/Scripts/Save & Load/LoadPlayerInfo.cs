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

    [Header("Score Requirements for Levels")]
    [SerializeField] private List<int> scoreRequirementsForLevels;

    private PlayerData playerData;

    private void OnEnable()
    {
        if (PlayerProfile.Instance != null)
        {
            PlayerProfile.Instance.LoadPlayerData();
            playerData = PlayerProfile.Instance.currentPlayerData;
        }

        CheckAndUnlockLevels();  // Check the player’s score and unlock levels if applicable
        LoadPlayerInfoToText();  // Load player information
        UpdateLevelButtons();    // Update button interactivity based on unlocked levels
    }

    private void LoadPlayerInfoToText()
    {
        if (playerData != null)
        {
            playerName.text = "Name: " + playerData.playerName;
            playerScore.text = "Total Score: " + playerData.totalScore;
            playerCollectibles.text = "Total Collectibles: " + playerData.collectibleItems;
            playerCompletedLevels.text = "Current Level: " + playerData.currentLevel;
        }
        else
        {
            Debug.LogWarning("Player data not loaded.");
        }
    }

    private void CheckAndUnlockLevels()
    {
        if (playerData == null || scoreRequirementsForLevels == null || scoreRequirementsForLevels.Count == 0) return;

        for (int i = 0; i < scoreRequirementsForLevels.Count; i++)
        {
            if (playerData.totalScore >= scoreRequirementsForLevels[i] && playerData.currentLevel <= i)
            {
                playerData.currentLevel = i + 1; // Unlocks up to the current level
                playerData.completedLevels = playerData.currentLevel;
            }
        }
    }

    private void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (i < playerData.currentLevel)
            {
                levelButtons[i].interactable = true;  
            }
            else
            {
                levelButtons[i].interactable = false; 
            }
        }
    }
}
