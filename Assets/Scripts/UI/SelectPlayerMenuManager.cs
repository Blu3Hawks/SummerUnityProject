using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectPlayerMenuManager : MonoBehaviour
{
    public GameObject levelSelectionMenu;
    public GameObject selectSkinMenu;
    public GameObject playerStatsMenu;

    public Image selectedPlayerSkinIndicator;

    PlayerData playerData;
    public CharacterDatabase characterDatabase;

    private void Start()
    {
        playerData = PlayerProfile.Instance.currentPlayerData;
        int currentPlayerIndexIndicator = PlayerProfile.Instance.currentPlayerData.selectedSkinIndex;
        Character selectedCharacter = characterDatabase.GetCharacter(currentPlayerIndexIndicator);
        selectedPlayerSkinIndicator.sprite = selectedCharacter.image;

    }
    public void DisableMenues()
    {
        levelSelectionMenu.SetActive(false);
        selectSkinMenu.SetActive(false);
        playerStatsMenu.SetActive(false);
    }

    public void LevelSelectionEnabled()
    {
        levelSelectionMenu.SetActive(true);
    }

    public void SelectSkinEnabled()
    {
        selectSkinMenu.SetActive(true);
    }

    public void PlayerStatsEnabled()
    {
        playerStatsMenu.SetActive(true);
    }

    public void SelectLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void SelectLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void SelectLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
}