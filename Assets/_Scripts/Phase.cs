using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    [SerializeField] public static int CurrentPhase = 1;

    [SerializeField] public static int CurrentRound = 1;

    [SerializeField] private static int threashold = 3;

    public static void ChangePhase()
    {
        CurrentPhase++;
    }

    public static void NextRound()
    {
        CurrentRound++;
        if (CurrentRound > threashold)
        {
            CurrentRound = 1;
            ChangePhase();
        }
    }

    public static void ResetGame()
    {
        CurrentPhase = 1;
        CurrentRound = 1;
    }
}
