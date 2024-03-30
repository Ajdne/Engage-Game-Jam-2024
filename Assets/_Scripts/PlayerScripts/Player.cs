using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Id;
    public int team; //0 = none; 1 = p1; 2 =p2;
    public int numberOfTiles; // Broj polja koja igrač poseduje
    public int numberOfSheep; // Broj ovaca koje igrač poseduje
    public bool isStunned; // Da li je igrač paralizovan

    // Konstruktor
    public Player(int team)
    {
        this.team = team;
        numberOfTiles = 0;
        numberOfSheep = 0;
        isStunned = false;
    }

    // Dodavanje poena za polja
    public void AddTilePoint()
    {
        numberOfTiles++;
    }

    // Dodavanje poena za ovce
    public void AddSheepPoint()
    {
        numberOfSheep++;
    }

    // Paralizovanje igrača
    public void Stun()
    {
        isStunned = true;
    }

    // Uklanjanje paralize
    public void Unstun()
    {
        isStunned = false;
    }
}
