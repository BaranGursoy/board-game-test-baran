using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    [SerializeField] private AudioSource playerBoardTouchAudioSource;
    [SerializeField] private AudioSource playerGetCurrencyAudioSource;

    private void Awake()
    {
        ActionHandler.PlayerTouchedTheBoard += PlayBoardTouchSFX;
        ActionHandler.PlayerStoppedOnItemTile += PlayGetCurrencySFX;
    }

    private void PlayBoardTouchSFX()
    {
        playerBoardTouchAudioSource.Play();
    }

    private void PlayGetCurrencySFX()
    {
        playerGetCurrencyAudioSource.Play();
    }
}
