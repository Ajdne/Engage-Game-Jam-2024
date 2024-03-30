using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    [SerializeField] public static int CurrentPhase = 1;

    [SerializeField] public static int CurrentRound = 1;

    // move number
    [SerializeField] public static int moveNumber = 1;

    [SerializeField] public static int threashold = 3;

    public static void ChangePhase()
    {
        CurrentPhase++;
        CalculateThreashold();
        CalculateMoveNumber();
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

    public static void CalculateThreashold()
    {
        if (CurrentPhase == 1)
        {
            threashold = 3;
        }
        else if (CurrentPhase == 2)
        {
            threashold = 4;
        }
        else if (CurrentPhase == 3)
        {
            threashold = 5;
        }
    }

    public static void CalculateMoveNumber()
    {
        if (CurrentPhase == 1)
        {
            moveNumber = 1;
        }
        else if (CurrentPhase == 2)
        {
            moveNumber = 2;
        }
        else if (CurrentPhase == 3)
        {
            moveNumber = 3;
        }
    }
    

    public static void ResetGame()
    {
        CurrentPhase = 1;
        CurrentRound = 1;
    }

    //make getter for everything
    public static int GetCurrentPhase()
    {
        return CurrentPhase;
    }

    public static int GetCurrentRound()
    {
        return CurrentRound;
    }

    public static int GetMoveNumber()
    {
        return moveNumber;
    }
}
