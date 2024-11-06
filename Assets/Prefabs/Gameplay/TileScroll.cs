using UnityEngine;
using UnityEngine.SceneManagement;

public class TileMovement : MonoBehaviour
{
    public float speed = 12f;                  // Base speed at which tiles move backwards
    public float resetPositionZ = -50f;        // Position Z where the tile gets reset
    public Vector3 spawnPosition = new Vector3(0, 0, 88); // New position after reset

    public float difficultySpeedModifier;
    private ObstacleSpawner obstacleSpawner;

    private void Start()
    {
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        obstacleSpawner.SpawnObstaclesOnLanes();

        //modify the speed based on level and difficulty
        SetDifficultySpeedModifier();
        speed += (float)PlayerProfile.Instance.currentPlayerData.difficulty * difficultySpeedModifier;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z <= resetPositionZ)
        {
            RespawnTile();
        }
    }

    private void SetDifficultySpeedModifier()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Level1":
                difficultySpeedModifier = 1f;
                break;
            case "Level2":
                difficultySpeedModifier = 1.5f;
                break;
            case "Level3":
                difficultySpeedModifier = 2.5f;
                break;
            default:
                difficultySpeedModifier = 1f;
                break;
        }
    }

    private void RespawnTile()
    {
        ClearObstacles();
        transform.position = spawnPosition;
        obstacleSpawner.SpawnObstaclesOnLanes();
    }

    private void ClearObstacles()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Obstacle"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
