using UnityEngine;

public class GameManager : NonPersistentSingleton<GameManager>
{
    [SerializeField] private Player dreamPlayer;
    public Player DreamPlayer => dreamPlayer;
    [SerializeField] private Player nightmarePlayer;
    public Player NightmarePlayer => nightmarePlayer;

    private void Awake()
    {
        Initialize();
    }

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
        //AudioManager.Instance.PlayStartGame();
        //AudioManager.Instance.PlayTheme();
        EventManager.startSFXEvent("Start");
        //EventManager.Instance.startSoundEvent("InGameTheme");
    }

    public void End()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        CalculateWinner();
        AmbientManager.Instance.ResetSkyboxMaterial();
    }
    private void CalculateWinner()
    {
        if (dreamPlayer.Points > nightmarePlayer.Points)
        {
            UIManager.instance.Winner.text = "Good Dream wins";
        }
        else if (dreamPlayer.Points < nightmarePlayer.Points)
        {
            UIManager.instance.Winner.text = "Nightmare wins";
        }
        else
        {
            UIManager.instance.Winner.text = "Sleep paralysis";
        }
        return;
    }

}
