using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the manager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Enforce the singleton pattern
        }
    }
    // GAME STAGES
    public static Action GameLoadedEvent;
    public static Action StartMovementEvent;

    //public static Action<KeyCode> InputTakenEvent;  // Called when button pressed
    public static Action<int> JumpOverEvent; // Invoked when the player is done moving
    public static Action RoundOverEvent; // Called when all command executed in Input Manager\

    public static Action PhaseOverEvent;

    public static Action GameOverEvent;

    public static Action SheepPickupEvent;

    public static Action<string> SoundEvent;

    public static Action<string> SFXEvent;

    //kad zoves ovaj event prosledjujes ime zvuka koji zelis da se cuje
    //Bump
    //Sheep
    //Start
    //End
    //Phase
    public void startSFXEvent(string SFXSound)
    {
        SFXEvent?.Invoke(SFXSound);
    }
    //ovo zoves za muziku kad je ubacis kasavice
    public void startSoundEvent(string SFXSound)
    {
        SoundEvent?.Invoke(SFXSound);
    }
}
