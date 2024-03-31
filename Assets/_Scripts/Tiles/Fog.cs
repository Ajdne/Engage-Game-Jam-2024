using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : Pickable
{
    protected override void OnPickUp(Player player)
    {
        ParticleManager.Instance.PlayFogParticle(this.transform.position);
    }
}
