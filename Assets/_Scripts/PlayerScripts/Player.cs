using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int team; //0 = none; 1 = p1; 2 =p2;
    public int Team => team;

    public int numberOfTiles; // Broj polja koja igrač poseduje
    public double numberOfSheep; // Broj ovaca koje igrač poseduje
    public bool isStunned; // Da li je igrač paralizovan

    public int Points;

    private CapsuleCollider _capCollider;
    private void Start()
    {
        _capCollider = GetComponent<CapsuleCollider>();
        _capCollider.enabled = false;
    }
    // Konstruktor
    public Player(int team)
    {
        this.team = team;
        numberOfTiles = 0;
        numberOfSheep = 0;
        isStunned = false;
    }

    private void OnEnable()
    {
        EventManager.GameLoadedEvent += ActivateCollider;
    }
    private void OnDisable()
    {
        EventManager.GameLoadedEvent -= ActivateCollider;
    }
    private void ActivateCollider()
    {
        _capCollider.enabled = true;
    }
    #region Effects
    // Dodavanje poena za polja
    public void AddTilePoint()
    {
        numberOfTiles++;
    }

    // Dodavanje poena za ovce
    public void AddSheepPoint(int a)
    {
        numberOfSheep += a;
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
    #endregion

    // calculate score
    public int CalculateScore()
    {
        Points = (int)numberOfTiles + (int)numberOfSheep;
        return Points;
    }

    public void LosePoints(int direction) // 0 = forward, 1 = back , 2 = right/left
    {
        if (direction == 0)
        {
            numberOfSheep *= 0.75;
        }
        else if (direction == 1)
        {
            numberOfSheep *= 0.1;
        }
        else if (direction == 2)
        {
            numberOfSheep *= 0.5;
        }
    }

    //make singleton
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}