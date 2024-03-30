using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : Pickable
{
    protected override void OnPickUp(Player player)
    {
        Debug.Log("Clear fog");
    }
}
