using JetBrains.Annotations;
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
    [SerializeField]
    public GameObject Ice;

    private void OnEnable()
    {
        EventManager.RoundOverEvent += SpawnSheep;
        EventManager.RoundOverEvent += Spawner;
    }
    private void OnDisable()
    {
        EventManager.RoundOverEvent -= SpawnSheep;
        EventManager.RoundOverEvent -= Spawner;
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
            Tile tile = SelectRandomTile();
            if (tile.IsPlayer || tile.IsFoggy) return;

            tile.SetFog();
            Instantiate(Fog, tile.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }        

    }
    public void ClearTiles()
    {
        int randomNumber = Random.Range(2, 5);

        for (int i = 0; i < randomNumber; i++)
        {
            Tile tile = SelectRandomTile();
            if (tile.IsPlayer) return;

            //Debug.Log("Obriso tile: " + tile.Team);

            int team = tile.Team;
            tile.RemoveTilePaint(team);
        }
    }
    public void SpawnSheep()
    {
        int randomNumber = Random.Range(1, 5);

        for (int i = 0; i < randomNumber; i++)
        {
            //Debug.Log("Ovca");
            Tile tile = SelectRandomTile();
            if (tile.IsPlayer || tile.IsPickable || tile.IsStun) return;

            tile.SetPicakble();

            GameObject tisSheep = Instantiate(Sheep, tile.transform.position + new Vector3(0, 1, 0), Quaternion.identity);


            tisSheep.transform.Rotate(new Vector3(0, 90* Random.Range(1, 5), 0));
        }
    }
    public void SpawnIce()
    {
        int randomNumber = Random.Range(1, 3);

        for (int i = 0; i < randomNumber; i++)
        {
            Tile tile = SelectRandomTile();
            if (tile.IsPlayer || tile.IsIcy) return;

            tile.Freeze();

            GameObject thisIce = Instantiate(Ice, tile.transform.position, Quaternion.identity);
        }
    }

    public void SpawnStun()
    {
        int randomNumber = Random.Range(1, 3);

        for (int i = 0; i < randomNumber; i++)
        {
            Tile tile = SelectRandomTile();
            if (tile.IsPlayer || tile.IsPickable || tile.IsStun || tile.IsIcy) return;

            tile.SetStun();
            Instantiate(Stun, tile.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }  
    }
    public void Spawner()
    {
        int currentPhase = PhaseManager.Instance.CurrentPhase;
        //int currentRound = PhaseManager.Instance.CurrentRound;
        switch (currentPhase)
        {
            case 1: //led
                SpawnIce();
                break;
            case 2: //magla
                SpawnFog();
                break;
            case 3: //stan
                SpawnStun();
                ClearTiles();
                break;
            case 4: //rem faza
                break;
        }
    }
}
