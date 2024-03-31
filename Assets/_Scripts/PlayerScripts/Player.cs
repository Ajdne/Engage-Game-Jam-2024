using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int team; //0 = none; 1 = p1; 2 =p2;
    public int Team => team;
[SerializeField] private TextMeshProUGUI scoreText; 
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

    // get tile number
    public int GetTileNumber()
    {
        return numberOfTiles;
    }

    public void LosePoints()
    {
        //lose 50% of points
        Points = Points / 2;
    }

    //update
    private void Update()
    {
        scoreText.text = CalculateScore().ToString();
    }
}