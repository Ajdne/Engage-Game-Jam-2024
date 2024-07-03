using System.Collections;
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

    private bool _isFoggy;
    public bool IsFoggy => _isFoggy;

    private bool _isPlayer;
    public bool IsPlayer => _isPlayer;

    private bool _isPickable;
    public bool IsPickable => _isPickable;

    private bool _isStun;
    public bool IsStun => _isStun;

    [SerializeField] private bool _isIcy;
    public bool IsIcy => _isIcy;

    public void Freeze()
    {
        _isIcy = true;
    }
    public void Occupied()
    {
        _isPlayer = true;
    }
    public void SetStun()
    {
        _isStun = true;
    }
    public void SetPicakble()
    {
        _isPickable = true;
    }
    public void SetFog()
    {
        _isFoggy = true;
    }

    private Renderer _renderer;

    public Player playerInstance;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        //get material components from both players, i will add them through unity edito
        _team = 0;
        _isFoggy = false;
        _isIcy = false;
        _isPickable = false;
        _isPlayer = false;
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
        if (other.TryGetComponent(out Player player))
        {
            //put tile movement here
            if (_isIcy)
            {
                player.GetComponent<PlayerMovement>().SlideMovement(transform.position);
                //add slide partcle
            }
            else
            {
                player.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
                PaintTile(player.Team);
                // Add paint particle
            }

            _isPlayer = true;
            _isPickable = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isPlayer = false;
        }
    }
}
