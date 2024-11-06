using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance;

    public PlayerData currentPlayerData;
    public CharacterDatabase characterDatabase;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
        LoadPlayerData();
    }


    public bool SaveFileExists()
    {
        return SaveSystem.LoadPlayer() != null;
    }

    public void LoadPlayerData()
    {
        currentPlayerData = SaveSystem.LoadPlayer();
        if (currentPlayerData != null)
        {
            Debug.Log("Player data loaded successfully.");
        }
        else
        {
            Debug.LogWarning("No saved player data found.");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SavePlayerData();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1" || scene.name == "Level2" || scene.name == "Level3")
        {
            InstantiateSelectedSkin();
        }
    }

    public void CreateNewPlayerData(PauseMenu settings, string playerName)
    {
        currentPlayerData = new PlayerData(settings, playerName);
        SavePlayerData();
    }

    public void SavePlayerData()
    {
        if (currentPlayerData != null)
        {
            SaveSystem.SavePlayer(currentPlayerData);
            Debug.Log("Player data saved successfully.");
        }
        else
        {
            Debug.LogWarning("Attempted to save null player data.");
        }
    }

    public void DeletePlayerData()
    {
        SaveSystem.DeleteSaveFile();
        currentPlayerData = null;
        Debug.Log("Player data deleted.");
    }

    private void InstantiateSelectedSkin()
    {
        if (currentPlayerData == null || characterDatabase == null)
        {
            Debug.LogWarning("Player data or character database is not set up correctly.");
            return;
        }

        int selectedSkinIndex = currentPlayerData.selectedSkinIndex;

        if (selectedSkinIndex >= 0 && selectedSkinIndex < characterDatabase.characterCount)
        {
            Character selectedCharacter = characterDatabase.GetCharacter(selectedSkinIndex);

            Player playerInstance = Instantiate(selectedCharacter.player);

            playerInstance.transform.position = new Vector3(0, 1.8f, 0);
            playerInstance.name = "PlayerCharacter";

            Debug.Log("Instantiated skin: " + selectedCharacter.skinName);
        }
        else
        {
            Debug.LogWarning("Invalid skin index.");
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SavePlayerData();
        }
    }
}
