using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int _team; //0 = none; 1 = p1; 2 =p2;
    public bool isFoggy;

    [SerializeField] private bool isIcy;
    public bool IsIcy => isIcy;

    public bool isPlayer;
    public bool isPickable;
    public bool isStun;

    private Renderer _renderer; 

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
    }

    private void PaintTile(int teamId)
    {
        switch (teamId)
        {
            case 0:
                RemoveTilePaint();
                break;
            case 1:
                _renderer.material.color = Color.red;
                Debug.Log("Color red");
                break;
            case 2:
                _renderer.material.color = Color.blue;
                Debug.Log("Color blue");
                break;
        }
    }

    public void RemoveTilePaint()
    {
        _renderer.material.color = Color.gray;
        Debug.Log("Color removed");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if(isIcy)
            {
                player.GetComponent<PlayerMovement>().SlideMovement(transform.position);
            }
            else if(_team != player.Team)
            {
                PaintTile(player.Team);
                player.AddTilePoint();
            }
        }
    }
}
