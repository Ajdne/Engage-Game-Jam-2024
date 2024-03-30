using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    public static List<Tile> tiles;
    [SerializeField]
    public GameObject Fog;
    [SerializeField]
    public GameObject Sheep;
    [SerializeField]
    public GameObject Stun;

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
        Instantiate(Fog, tile.transform.position, Quaternion.identity);
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
        Instantiate(Sheep, tile.transform.position, Quaternion.identity);
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
        Instantiate(Stun, tile.transform.position, Quaternion.identity);
    }
}
