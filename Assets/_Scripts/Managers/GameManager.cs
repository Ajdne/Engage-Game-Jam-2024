using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
