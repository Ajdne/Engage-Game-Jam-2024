using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Pickable
{
    [SerializeField] private int sheepValue = 1; // Vrednost ovce
    protected override void OnPickUp(Player player)
    {
        player.AddSheepPoint(sheepValue); // Povećavamo skor za ovce igrača
        Debug.Log("Player " + player.Id + " got sheep");
    }
}
