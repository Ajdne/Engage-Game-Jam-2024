﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material dreamTileMaterial;
    [SerializeField] private Material nightmareTileMaterial;

    [SerializeField] private Material defaultMaterial;

    private int _team; //0 = none; 1 = p1; 2 =p2;
    public int Team => _team;
    public bool isFoggy;

    [SerializeField] private bool isIcy;
    public bool IsIcy => isIcy;

    public void Freeze()
    {
        isIcy = true;
    }

    public bool isPlayer;
    public bool isPickable;
    public bool isStun;

    private Renderer _renderer; 

    public Player playerInstance;

    // Konstruktor
    public Tile()
    {
        _team = 0;
        isFoggy = false;
        isIcy = false;
        isPickable = false;
        isPlayer = false;
    }
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        //get material components from both players, i will add them through unity edito

    }

    private void PaintTile(int teamId)
    {
        switch (teamId)
        {
            case 0:
                RemoveTilePaint(teamId);
                break;
            case 1:
                _renderer.material = dreamTileMaterial;
                if (_team == 2)
                {
                    GameManager.Instance.NightmarePlayer.numberOfTiles -= 1;
                }
                _team = 1;
                GameManager.Instance.DreamPlayer.numberOfTiles += 1;
                break;
            case 2:
                _renderer.material = nightmareTileMaterial;
                if (_team == 1)
                {
                    GameManager.Instance.DreamPlayer.numberOfTiles -= 1;
                }
                GameManager.Instance.NightmarePlayer.numberOfTiles += 1;
                _team = 2;
                break;
        }
    }

    public void RemoveTilePaint(int teamId)
    {
        _renderer.material = defaultMaterial;
        if (teamId == 1)
        {
            GameManager.Instance.DreamPlayer.numberOfTiles -= 1;
        }
        else if (teamId == 2)
        {
            GameManager.Instance.NightmarePlayer.numberOfTiles -= 1;
        }
        _team = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            //put tile movement here
            if(isIcy)
            {
                player.GetComponent<PlayerMovement>().SlideMovement(transform.position);
                //add slide partcle
            }
            else if(_team != player.Team)
            {
                PaintTile(player.Team);
                //add paint particle
            }
            isPlayer = true;
            isPickable = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            isPlayer = false;
        }
    }
}
