using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    public Image selectedPlayerImage;
    public CharacterDatabase characterDatabase;
    public TextMeshProUGUI characterNameText;

    public Image selectedCharacterIndicator;

    private int selectedCharacterIndex = 0;
    private PlayerData playerData;

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
        }
    }

    private void Start()
    {
        playerData = PlayerProfile.Instance.currentPlayerData;

        Debug.Log("Loaded player data with selectedSkinIndex: " + playerData.selectedSkinIndex);

        selectedCharacterIndex = playerData.selectedSkinIndex;

        UpdateCharacterSelection();
    }

    public void OnNextButton()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterDatabase.characterCount)
        {
            selectedCharacterIndex = 0;
        }
        UpdateCharacterSelection();
    }

    public void OnPreviousButton()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterDatabase.characterCount - 1;
        }
        UpdateCharacterSelection();
    }

    private void UpdateCharacterSelection()
    {
        Character selectedCharacter = characterDatabase.GetCharacter(selectedCharacterIndex);

        selectedPlayerImage.sprite = selectedCharacter.image;
        characterNameText.text = selectedCharacter.skinName;

        Debug.Log("Selected Character: " + selectedCharacter.skinName + " (Index: " + selectedCharacterIndex + ")");
    }

    public void SelectSkin()
    {
        playerData.selectedSkinIndex = selectedCharacterIndex;

        Character selectedCharacter = characterDatabase.GetCharacter(selectedCharacterIndex);
        selectedCharacterIndicator.sprite = selectedCharacter.image;

        Debug.Log("Saving selectedSkinIndex: " + selectedCharacterIndex);

        PlayerProfile.Instance.currentPlayerData.selectedSkinIndex = selectedCharacterIndex;

        Debug.Log("Skin selected and saved: " + selectedCharacter.skinName);
    }
}
