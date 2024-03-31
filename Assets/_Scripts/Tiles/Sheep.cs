using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Pickable
{
    [SerializeField] private int sheepValue = 1; // Vrednost ovce
    protected override void OnPickUp(Player player)
    {
        player.AddSheepPoint(sheepValue); // Povećavamo skor za ovce igrača
        AudioManager.Instance.PlaySheepSound(); // Pokrećemo zvuk ovce
        ParticleManager.Instance.PlaySheepParticle(this.transform.position); // Pokrećemo česticu ovce
    }

    //on awake, play particle
    private void Awake()
    {
        ParticleManager.Instance.PlaySheepSpawnParticle(this.transform.position);
    }
}
