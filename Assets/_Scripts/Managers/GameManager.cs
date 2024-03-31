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
        new WaitForSecondsRealtime(2);
        Time.timeScale = 0.2f;
    }
    private void CalculateWinner()
    {
        if (dreamPlayer.Points > nightmarePlayer.Points)
        {
            UIManager.instance.Winner.text = "Nightmare wins";
        }
        else if (dreamPlayer.Points < nightmarePlayer.Points)
        {
            UIManager.instance.Winner.text = "Nightmare wins";
        }
        else
        {
            UIManager.instance.Winner.text = "Sleep paralysis";
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
