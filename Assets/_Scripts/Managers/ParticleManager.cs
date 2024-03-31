using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    [SerializeField] private GameObject _bumpParticle;
    [SerializeField] private GameObject _slideParticle;

    [SerializeField] private GameObject _stunParticle;
    [SerializeField] private GameObject _fogParticle;

    [SerializeField] private GameObject _sheepParticle;
    [SerializeField] private GameObject _sheepSpawnParticle;

    //sheepspawnparticle
    public void PlaySheepSpawnParticle(Vector3 position)
    {
        SpawnParticles(_sheepSpawnParticle, position);
    }
    

    //play bump particle
    public void PlayBumpParticle(Vector3 position)
    {
        SpawnParticles(_bumpParticle, position);
    }

    //play slide particle
    public void PlaySlideParticle(Vector3 position)
    {
        SpawnParticles(_slideParticle, position);
    }

    //play stun particle
    public void PlayStunParticle(Vector3 position)
    {
        SpawnParticles(_stunParticle, position);
    }

    // play fog particle
    public void PlayFogParticle(Vector3 position)
    {
        SpawnParticles(_fogParticle, position);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SpawnParticles(GameObject particle, Vector3 position)
    {
        Instantiate(particle, position, Quaternion.identity);
    }

    //play sheep particle
    public void PlaySheepParticle(Vector3 position)
    {
        SpawnParticles(_sheepParticle, position);
    }
}
