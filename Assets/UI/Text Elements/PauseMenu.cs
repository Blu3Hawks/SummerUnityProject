using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject settingsMenuUI;
    public static bool gameIsPaused = false;
    public AudioMixer audioMixer;

    [Header("Input Systems")]
    [SerializeField] private GameObject input_Swipes;
    [SerializeField] private GameObject input_Joystick;
    [SerializeField] private GameObject input_Buttons;

    [Header("UI Elements")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private TMP_Dropdown inputChoiceDropdown;


    [Header("Saved Values")]
    public float volume;
    public int systemIndex;

    public enum Difficulty
    {
        easy,
        normal,
        hard
    }

    public Difficulty difficulty;


    private void Start()
    {
        SettingUpUI();
        LoadSettings();
    }

    private void LoadSettings()
    {
        //loading the values
        PlayerData data = SaveSystem.LoadPlayer();
        volume = data.volume;
        systemIndex = data.inputSystem;
        difficulty = data.diffculty;
        //making sure the values would implement to the UI properly
        volumeSlider.value = volume;
        difficultyDropdown.value = (int)difficulty;
        inputChoiceDropdown.value = systemIndex;

        Debug.Log(volume + systemIndex.ToString() + difficulty);
    }

    private void SettingUpUI()
    {
        //setting the menues properly
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        settingsButton.SetActive(true);

        //setting the input system properly to default
        input_Swipes.SetActive(true);
        input_Joystick.SetActive(false);
        input_Buttons.SetActive(false);
    }

  

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsButton.SetActive(true);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        settingsButton.SetActive(false);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void SettingsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        SaveSystem.SaveSettings(this);

    }

    public void SelectDifficulty(int index)
    {
        switch (index)
        {
            case 0:
                difficulty = Difficulty.easy;
                Debug.Log(difficulty);
                systemIndex = 0;
                break;
            case 1:
                difficulty = Difficulty.normal;
                Debug.Log(difficulty);
                systemIndex = 1;
                break;
            case 2:
                difficulty = Difficulty.hard;
                Debug.Log(difficulty);
                systemIndex = 2;
                break;
        }
        SaveSystem.SaveSettings(this);
    }
    public void SelectInputSystem(int index)
    {
        switch (index)
        {
            case 0:
                input_Swipes.SetActive(true);
                input_Joystick.SetActive(false);
                input_Buttons.SetActive(false);
                Debug.Log("Swipes is now the input choice");
                break;
            case 1:
                input_Swipes.SetActive(false);
                input_Joystick.SetActive(true);
                input_Buttons.SetActive(false);
                Debug.Log("Joystick is now the input choice");
                break;
            case 2:
                input_Swipes.SetActive(false);
                input_Joystick.SetActive(false);
                input_Buttons.SetActive(true); 
                Debug.Log("Buttons is now the input choice");
                break;
        }
        SaveSystem.SaveSettings(this);

    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        float currentVolume;
        if (audioMixer.GetFloat("Volume", out currentVolume))
        {
            this.volume = currentVolume;
        }
        SaveSystem.SaveSettings(this);

    }
}
