[System.Serializable]
public class PlayerData
{
    public string playerName;
    public PauseMenu.Difficulty difficulty;
    public float volume;
    public int inputSystem;
    public int totalScore;
    public int currentLevel;
    public int completedLevels;
    public int collectibleItems;
    public int selectedSkinIndex;

    public PlayerData(PauseMenu playerSettings, string name) //default values added
    {
        playerSettings.difficulty = difficulty;
        playerSettings.volume = volume;
        playerSettings.systemIndex = inputSystem;
        playerName = name;
        totalScore = 0;
        currentLevel = 1;
        completedLevels = 0;
        collectibleItems = 0;
        selectedSkinIndex = 0;
    }
}
