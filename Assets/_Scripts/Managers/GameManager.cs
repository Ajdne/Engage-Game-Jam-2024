using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Player dreamPlayer;
    public Player DreamPlayer => dreamPlayer;
    [SerializeField] private Player nightmarePlayer;
    public Player NightmarePlayer => nightmarePlayer;

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

    private void Start()
    {
        Phase.ResetGame();
    }

    public void End()
    {
        Debug.Log("Game Over");
        CalculateWinner();
        //EventManager.GameOverEvent?.Invoke();
    }

    private void CalculateWinner()
    {
        if (dreamPlayer.Points > nightmarePlayer.Points)
        {
            Debug.Log("Dream player wins");
        }
        else if (dreamPlayer.Points < nightmarePlayer.Points)
        {
            Debug.Log("Nightmare player wins");
        }
        else
        {
            Debug.Log("Paraliza sna Event");
        }
    }
}
