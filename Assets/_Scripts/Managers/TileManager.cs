using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    public static List<Tile> tiles;

    public static Tile SelectRandomTile()
    {
        int randomIndex = Random.Range(0, tiles.Count);
        Tile tile = tiles[randomIndex];

        return tile;
    }
}
