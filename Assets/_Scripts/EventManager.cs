using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action GameLoadedEvent;
    public static Action StartMovementEvent;

    //public static Action<KeyCode> InputTakenEvent;  // Called when button pressed
    public static Action<int> JumpOverEvent; // Invoked when the player is done moving
    public static Action MovementOverEvent; // Called when players finished moving


}
