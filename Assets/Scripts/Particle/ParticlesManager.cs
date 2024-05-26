using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem appleParticles;
    [SerializeField] private ParticleSystem pearParticles;
    [SerializeField] private ParticleSystem strawberryParticles;
    private void Awake()
    {
        //ActionHandler.SpawnCurrency += SpawnParticlesAtPosition;
    }

    private void SpawnParticlesAtPosition(ItemType itemType, Vector3 particleSpawnPosition)
    {
        ParticleSystem particleSystemToPlay = null;

        switch (itemType)
        {
            case ItemType.Apple:
                particleSystemToPlay = appleParticles;
                break;
            case ItemType.Pear:
                particleSystemToPlay = pearParticles;
                break;
            case ItemType.Strawberry:
                particleSystemToPlay = strawberryParticles;
                break;
        }

        if (!particleSystemToPlay) return;

        particleSystemToPlay.transform.position = particleSpawnPosition;
        particleSystemToPlay.Play();
    }
}
