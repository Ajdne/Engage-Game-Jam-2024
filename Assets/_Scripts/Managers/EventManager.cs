using System;
using UnityEngine;

public class EventManager : MonoBehaviour
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
}
