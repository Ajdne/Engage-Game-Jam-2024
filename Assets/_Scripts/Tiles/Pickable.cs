using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int Id;
    public bool isSheep;
    public bool isStun;

    // Konstruktor
    public Pickable()
    {
        isSheep = false;
        isStun = false;
    }
}
