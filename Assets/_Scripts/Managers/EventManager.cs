using System;
using UnityEngine;

public static class EventManager
{
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
    public static void startSFXEvent(string SFXSound)
    {
        SFXEvent?.Invoke(SFXSound);
    }
    //ovo zoves za muziku kad je ubacis kasavice
    public static void startSoundEvent(string SFXSound)
    {
        SoundEvent?.Invoke(SFXSound);
    }
}
