using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private int _currentPhase;
    private int _currentRound;

    [SerializeField] private List<Phase> phases = new List<Phase>();

    private void OnEnable()
    {
        EventManager.StartMovementEvent += NextRound;
    }
    private void OnDisable()
    {
        EventManager.StartMovementEvent -= NextRound;
    }

    public int GetMaxMoves()
    {
        print("Rounds  " + phases[_currentPhase].Rounds[_currentRound]);
        return phases[_currentPhase].Rounds[_currentRound].MaxMoves;
    }
    public void NextRound()
    {
        if (_currentRound < phases[_currentPhase].Rounds.Count-1)
        {
            _currentRound++;
            //print("Rounds  " + phases[_currentPhase].Rounds[_currentRound]);
        }
        else
        {
            _currentRound = 0;  // Reset counter
            ChangePhase();
        }
    }
    private void ChangePhase()
    {
        if (_currentPhase < phases.Count-1)
        {
            _currentPhase++;
        }
        else GameManager.Instance.End();
    }
}

[System.Serializable]
public class Phase
{
    public List<Rounds> Rounds;
}

[System.Serializable]
public class Rounds
{
    //public int RoundNumber;
    public int MaxMoves;
}
