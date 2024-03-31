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

    private void OnEnable()
    {
        EventManager.RoundOverEvent += SpawnSheep;
    }
    private void OnDisable()
    {
        EventManager.RoundOverEvent -= SpawnSheep;
    }

    public Tile SelectRandomTile()
    {
        int randomIndex = Random.Range(0, tiles.Count);
        Tile tile = tiles[randomIndex];

        return tile;
    }
    public void SpawnFog()
    {
        int randomNumber = Random.Range(1, 4);

        for (int i = 0; i < randomNumber; i++)
        {
            Tile tile = new Tile();
            tile = SelectRandomTile();
            if (tile.isPlayer || tile.isFoggy) return;

            tile.isFoggy = true;
            Instantiate(Fog, tile.transform.position, Quaternion.identity);
        }        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFog();
        }
    }
    public void SpawnSheep()
    {
        int randomNumber = Random.Range(1, 5);

        for (int i = 0; i < randomNumber; i++)
        {
            Tile tile = new Tile();
            tile = SelectRandomTile();
            if (tile.isPlayer || tile.isPickable || tile.isStun) return;

            tile.isPickable = true;

            GameObject tisSheep = Instantiate(Sheep, tile.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

            tisSheep.transform.Rotate(new Vector3(0, 90* Random.Range(1, 5), 0));
        }
    }

    public void SpawnStun()
    {
        int randomNumber = Random.Range(1, 3);

        for (int i = 0; i < randomNumber; i++)
        {
            Tile tile = new Tile();
            tile = SelectRandomTile();
            if (tile.isPlayer || tile.isPickable || tile.isStun) return;

            tile.isStun = true;
            Instantiate(Stun, tile.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }  
    }

}
