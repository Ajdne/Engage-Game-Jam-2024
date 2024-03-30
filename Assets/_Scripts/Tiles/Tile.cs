using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Id;
    public int team; //0 = none; 1 = p1; 2 =p2;
    public bool isFoggy;
    public bool isIcy;
    // Konstruktor
    public Tile()
    {
        team = 0;
        isFoggy = false;
        isIcy = false;
    }
}
