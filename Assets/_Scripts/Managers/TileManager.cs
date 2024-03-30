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
    public void spawnFog()
    {
        Tile tile = new Tile();
        //selektuj tile bez playera
        do
        {
            tile = SelectRandomTile();
        } while (tile.isPlayer);

        //stvoriti fog iznad ovog tilea
        tile.isFoggy = true;
    }
    public void spawnSheep()
    {
        Tile tile = new Tile();
        //selektuj tile bez playera
        do
        {
            tile = SelectRandomTile();
        } while (tile.isPlayer || tile.isPickable || tile.isStun);

        //stvoriti fog iznad ovog tilea
        tile.isPickable = true;
    }
    public void spawnStun()
    {
        Tile tile = new Tile();
        //selektuj tile bez playera
        do
        {
            tile = SelectRandomTile();
        } while (tile.isPlayer || tile.isPickable || tile.isStun);

        //stvoriti fog iznad ovog tilea
        tile.isStun = true;
    }
}
