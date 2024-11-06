using UnityEngine;
using TMPro;  

public class PlayerNameInputUI : MonoBehaviour
{
    public TMP_InputField nameInputField;  
    public PauseMenu pauseMenu; 

    public void SubmitName()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerProfile.Instance.CreateNewPlayerData(pauseMenu, playerName); 
            Debug.Log($"Player name '{playerName}' has been saved.");
        }
        else
        {
            Debug.LogWarning("Player name is empty! Please enter a valid name.");
        }
    }
}
