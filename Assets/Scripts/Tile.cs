using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType type;

}

public enum TileType
{
    NormalTile,
    ObstacleTile,
    PowerupTile
}