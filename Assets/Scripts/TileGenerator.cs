using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] GameObject startingTile;
    [SerializeField] List<GameObject> obstacles;

    private Vector3 tileLocation = Vector3.zero;
    private Vector3 tileDirection = Vector3.forward;
    private GameObject previousTile;
    private int tileStartCount = 15;

    private List<GameObject> currentTiles;
    private List<GameObject> currentObstacles;

    private void Start()
    {
        currentObstacles = new List<GameObject>();
        currentTiles = new List<GameObject>();

        Random.InitState(System.DateTime.Now.Millisecond);

        for (int i = 0; i < tileStartCount; i++)
        {
            SpawnTile(startingTile.GetComponent<Tile>(), false, false);
        }
    }

    private void Update()
    {
        //SpawnTile(startingTile.GetComponent<Tile>(), false, false);
    }


    private void SpawnTile(Tile tile, bool spawnObstacle, bool spawnPowerup)
    {
        previousTile = GameObject.Instantiate(tile.gameObject, tileLocation, Quaternion.identity);
        currentTiles.Add(previousTile);
        tileLocation += Vector3.Scale(previousTile.GetComponent<Renderer>().bounds.size, tileDirection); //spawns next tile at previous tile's edge
    }
}
