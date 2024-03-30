using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpawner : MonoBehaviour
{
    Tile tile = new Tile();

    public void spawnFog()
    {
        Tile randomTile = SelectRandomTile(GameManager.Instance.tiles);
        //stvoriti fog iznad ovog tilea
    }
    Tile SelectRandomTile(List<Tile> objectList)
    {
        do
        {
            int randomIndex = Random.Range(0, objectList.Count);
            Tile tile = objectList[randomIndex];
        } while (tile.isPlayer);

        return tile;
    }
}
