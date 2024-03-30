using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpawner : MonoBehaviour
{
    private Tile tile = new Tile();
    public void spawnFog()
    {
        //selektuj tile bez playera
        do
        {
            tile = TileManager.SelectRandomTile();
        } while (tile.isPlayer);

        //stvoriti fog iznad ovog tilea
        tile.isFoggy = true;
    }

}
