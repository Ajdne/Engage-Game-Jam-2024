using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int timeForInput = 3;
    public int TimeForInput => timeForInput;

    #region Singleton
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
    private void Start()
    {
        Phase = 1;
        round = 1;
    }
    #endregion
    public int Phase;
    public int round;

    // Metoda za promenu faze
    public void ChangePhase()
    {
        Phase++;
    }

    // Metoda za promenu runde
    public void NextRound()
    {
        round++;
    }

}
