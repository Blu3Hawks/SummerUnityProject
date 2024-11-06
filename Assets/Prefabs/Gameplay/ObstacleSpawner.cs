using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    [SerializeField] private float minLaneOffset = 3f;
    [SerializeField] private float maxLaneOffset = 3.5f;
    [SerializeField] private float minObstacleSpacing = 1f;
    [SerializeField] private float obstacleSpawnZPosition = 0f;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private float tileLength = 50f;
    [SerializeField] private float addedYPos = 1.5f;

    public void SpawnObstaclesOnLanes()
    {
        int amountOfRows = Mathf.RoundToInt(tileLength / spawnInterval);

        for (int i = 0; i < amountOfRows; i++)
        {
            float currentSpawnZ = obstacleSpawnZPosition + i * spawnInterval;
            List<Vector3> possibleSpawnLocations = new List<Vector3>
            {
                new Vector3(-Random.Range(minLaneOffset, maxLaneOffset), addedYPos, currentSpawnZ),
                new Vector3(0, addedYPos, currentSpawnZ),
                new Vector3(Random.Range(minLaneOffset, maxLaneOffset), addedYPos, currentSpawnZ)
            };

            int amountOfSpawns = Random.Range(0, 3); 

            for (int j = 0; j < amountOfSpawns; j++)
            {
                if (possibleSpawnLocations.Count == 0)
                    break;

                int randomIndex = Random.Range(0, possibleSpawnLocations.Count);
                Vector3 spawnPosition = possibleSpawnLocations[randomIndex];

                GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                Instantiate(obstaclePrefab, transform.position + spawnPosition, Quaternion.identity, transform);

                possibleSpawnLocations.RemoveAt(randomIndex);
            }
        }
    }
}
