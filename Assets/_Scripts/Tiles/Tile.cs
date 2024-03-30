using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Id;
    public int team; //0 = none; 1 = p1; 2 =p2;
    public bool isFoggy;
    public bool isIcy;
    public bool isPlayer;
    public bool isPickable;
    public bool isStun;
    // Konstruktor
    public Tile()
    {
        team = 0;
        isFoggy = false;
        isIcy = false;
        isPickable = false;
        isPlayer = false;
    }
}
