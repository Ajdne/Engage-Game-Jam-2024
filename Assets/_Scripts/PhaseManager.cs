using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;
    private int _currentPhase;
    private int _currentRound;

    public int CurrentPhase => _currentPhase;
    public int CurrentRound => _currentRound;

    [SerializeField] private List<Phase> phases = new List<Phase>();

    private void OnEnable()
    {
        EventManager.StartMovementEvent += NextRound;
    }
    private void OnDisable()
    {
        EventManager.StartMovementEvent -= NextRound;
    }
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

    public int GetMaxMoves()
    {
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
            EventManager.PhaseOverEvent?.Invoke();
            AudioManager.Instance.PlayPhaseChange();
        }
        else GameManager.Instance.End();
    }

    //getphase currentphase
    public Phase GetCurrentPhase()
    {
        return phases[_currentPhase];
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
