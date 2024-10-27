using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float speed  = 12f;               // Speed at which tiles move backwards
    public float resetPositionZ = -50f;      // Position Z where the tile gets reset
    public Vector3 spawnPosition = new Vector3(0, 0, 88); // New position after reset

    private ObstacleSpawner obstacleSpawner;

    private void Start()
    {
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        obstacleSpawner.SpawnObstaclesOnLanes();
    }

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z <= resetPositionZ)
        {
            RespawnTile();
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
        //remove any child components - obstacles
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Obstacle")) 
            {
                Destroy(child.gameObject);
            }
        }
    }
}
