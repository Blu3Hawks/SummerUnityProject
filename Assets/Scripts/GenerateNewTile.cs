using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class GenerateNewTile : MonoBehaviour
{
    public Tile nextTile;
    public GameObject newTile;
    public Vector3 spawnPosition = new Vector3 (0, 0, 0);
    private GameObject previousTile;

    [SerializeField] List<GameObject> obstacles;
    [SerializeField] List<GameObject> powerUps;
    private List<GameObject> currentObstacles;
    private List<GameObject> currentPowerUps;

    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Trigger"))
        {
            SpawnTile(nextTile, false, false);
        }
    }

    private void SpawnTile(Tile tile, bool _spawnObstacle, bool _spawnPowerup)
    {
        Instantiate(newTile, spawnPosition, Quaternion.identity);
        spawnPosition += Vector3.Scale(previousTile.GetComponent<Renderer>().bounds.size, Vector3.forward); //spawns next tile at previous tile's edge

    }
}
