using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : NonPersistentSingleton<PhaseManager>
{
    private int _currentPhase;

    private int _currentRound;

    public int CurrentPhase => _currentPhase;
    public int CurrentRound => _currentRound;

    private void Awake() {
        Initialize();
    }

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
            //AudioManager.Instance.PlayPhaseChange
            EventManager.startSFXEvent("Phase");
        }
        /*else if (_currentPhase == phases.Count-1) 
        {
            if(GameManager.Instance.DreamPlayer.CalculateScore() > GameManager.Instance.NightmarePlayer.CalculateScore())
            {
                //impelement realtime movement
            }
            else
            {
                //implement fast movement + falling
            }
            _currentPhase++;
            EventManager.PhaseOverEvent?.Invoke();
            AudioManager.Instance.PlayPhaseChange();
        }*/
        else EventManager.GameOverEvent?.Invoke();
    }

    //getphase currentphase
    public static int GetCurrentPhase()
    {
        return Instance._currentPhase;
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
