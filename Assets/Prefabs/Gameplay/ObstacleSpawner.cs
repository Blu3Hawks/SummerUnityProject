using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    [SerializeField] private float minLaneOffset;
    [SerializeField] private float maxLaneOffset;
    [SerializeField] private float minObstacleSpacing;
    [SerializeField] private float obstacleSpawnZPosition;

    // 0 is the middle option - 3.2 are the sides, so 3.2 and -3.2. It can also be between 3 and 3.5 to randomise it. 

    public void SpawnObstaclesOnLanes()
    {
        List<float> lanePositions = new List<float> {Random.Range(-minLaneOffset, -maxLaneOffset), 0, Random.Range(minLaneOffset, maxLaneOffset)};

        lanePositions.Shuffle();

        float lastSpawnX = float.MinValue;

        foreach (float laneX in lanePositions)
        {
            if (Random.value > 0.5f)
            {
                if (Mathf.Abs(laneX - lastSpawnX) >= minObstacleSpacing)
                {
                    GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

                    Vector3 spawnPosition = new Vector3(laneX, obstacleSpawnZPosition, 0);
                    Instantiate(obstaclePrefab, transform.position + spawnPosition, Quaternion.identity, transform);

                    lastSpawnX = laneX;
                }
            }
        }
    }
}
