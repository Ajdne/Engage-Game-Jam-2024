﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Pickable
{
    protected override void OnPickUp(Player player)
    {
        // Logika za stavljanje igrača u stanje paralize, ako je potrebno
        player.Stun();
        Debug.Log("Player "+player.Id+" stun");
    }
}
