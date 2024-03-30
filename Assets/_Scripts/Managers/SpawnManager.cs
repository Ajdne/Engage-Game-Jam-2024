using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public List<Tile> tiles = new();
    [SerializeField]
    public GameObject Fog;
    [SerializeField]
    public GameObject Sheep;
    [SerializeField]
    public GameObject Stun;

    public Tile SelectRandomTile()
    {
        int randomIndex = Random.Range(0, tiles.Count);
        Tile tile = tiles[randomIndex];

        return tile;
    }
    public void SpawnFog()
    {
        Tile tile = new Tile();
        //selektuj tile bez playera
        do
        {
            tile = SelectRandomTile();
        } while (tile.isPlayer || tile.isFoggy);

        //stvoriti fog iznad ovog tilea
        tile.isFoggy = true;
        Instantiate(Fog, tile.transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFog();
        }
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
