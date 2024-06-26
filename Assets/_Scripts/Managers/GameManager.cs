using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private Player dreamPlayer;
    public Player DreamPlayer => dreamPlayer;
    [SerializeField] private Player nightmarePlayer;
    public Player NightmarePlayer => nightmarePlayer;

    #region Singleton
    private void Awake()
    {
        Initialize();
    }
    #endregion

    private void OnEnable()
    {
        EventManager.GameOverEvent += End;
    }
    private void OnDisable()
    {
        EventManager.GameOverEvent -= End;
    }

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
        AmbientManager.Instance.ResetSkyboxMaterial();

        Time.timeScale = 0f;
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

}
