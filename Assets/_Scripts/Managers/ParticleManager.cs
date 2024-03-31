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
    

    //play bump particle
    public void PlayBumpParticle(Vector3 position)
    {
        SpawnParticles(_bumpParticle, position);
    }

    //play slide particle
    public void PlaySlideParticle()
    {
        PlayParticle(_slideParticle);
    }

    //play stun particle
    public void PlayStunParticle()
    {
        PlayParticle(_stunParticle);
    }

    // play fog particle
    public void PlayFogParticle()
    {
        PlayParticle(_fogParticle);
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
    private void PlayParticle(GameObject parPrefab)
    {
        Instantiate(parPrefab, Vector3.zero, Quaternion.identity);
    }

    public void SpawnParticles(GameObject particle, Vector3 position)
    {
        Instantiate(particle, position, Quaternion.identity);
    }
}
