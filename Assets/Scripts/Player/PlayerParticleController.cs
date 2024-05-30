using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem playerWonItemParticleSystem;

    private void Awake()
    {
        GameActions.PlayerStoppedOnItemTile += PlayItemWonParticles;
    }

    private void OnDestroy()
    {
        GameActions.PlayerStoppedOnItemTile -= PlayItemWonParticles;
    }

    private void PlayItemWonParticles()
    {
        playerWonItemParticleSystem.Play();
    }
}
