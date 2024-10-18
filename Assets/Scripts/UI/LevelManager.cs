using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerData playerData;

    public void CompleteLevel()
    {
        playerData.completedLevels++;
        playerData.currentLevel++; 
        SaveSystem.SavePlayer(playerData);  // Save progress after level completion
    }

    public void ReplayLevel(int levelIndex)
    {
        if (levelIndex <= playerData.completedLevels)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + levelIndex);
        }
        else
        {
            Debug.LogWarning("Level not unlocked yet!");
        }
    }
}
