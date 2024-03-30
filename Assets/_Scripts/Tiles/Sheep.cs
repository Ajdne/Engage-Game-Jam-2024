using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Pickable
{
    protected override void OnPickUp(Player player)
    {
        player.numberOfSheep++; // Povećavamo skor za ovce igrača
    }
}
