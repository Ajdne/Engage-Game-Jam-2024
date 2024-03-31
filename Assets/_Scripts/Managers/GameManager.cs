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
        Time.timeScale = 1f;
        AudioManager.Instance.PlayStartGame();
        AudioManager.Instance.PlayTheme();
    }

    public void End()
    {
        Debug.Log("Game Over");
        AudioManager.Instance.StopAudio();
        AudioManager.Instance.PlayEnd();
        CalculateWinner();
        Time.timeScale = 0.0f;
    }
    public int Winner;
    private void CalculateWinner()
    {
        if (dreamPlayer.Points > nightmarePlayer.Points)
        {
            Debug.Log("Dream player wins");
            Winner = 1;
        }
        else if (dreamPlayer.Points < nightmarePlayer.Points)
        {
            Winner = 2;
            Debug.Log("Nightmare player wins");
        }
        else
        {
            Debug.Log("Paraliza sna Event");
        }
    }
    private void OnEnable()
    {
        EventManager.GameOverEvent += End;
    }
    private void OnDisable()
    {
        EventManager.GameOverEvent -= End;
    }
}
