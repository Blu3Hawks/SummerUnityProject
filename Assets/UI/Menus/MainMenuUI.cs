using UnityEngine;
using UnityEngine.SceneManagement;  
using TMPro;  

public class MainMenuUI : MonoBehaviour
{
    public TMP_InputField playerNameInputField; 
    public GameObject newGameButton;
    public GameObject loadGameButton;
    public GameObject deleteSaveButton;
    public GameObject nameInputUI;  

    private void Start()
    {
        if (PlayerProfile.Instance.SaveFileExists())
        {
            loadGameButton.SetActive(true);  
            deleteSaveButton.SetActive(true);  
            nameInputUI.SetActive(false); 
        }
        else
        {
            loadGameButton.SetActive(false);  
            deleteSaveButton.SetActive(false);  
            nameInputUI.SetActive(true);  
        }
    }

    public void StartNewGame()
    {
        string playerName = playerNameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerProfile.Instance.CreateNewPlayerData(new PauseMenu(), playerName); 
            SceneManager.LoadScene("SelectPlayer");  
        }
        else
        {
            Debug.LogWarning("Player name is empty! Please enter a valid name.");
            return;
        }
    }

    public void LoadGame()
    {
        PlayerProfile.Instance.LoadPlayerData();
        SceneManager.LoadScene("SelectPlayer");  
    }

    public void DeleteSave()
    {
        PlayerProfile.Instance.DeletePlayerData();
        Start(); 
    }
}
