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
        GameActions.PlayerTouchedTheBoard += PlayBoardTouchSFX;
        GameActions.PlayerStoppedOnItemTile += PlayGetCurrencySFX;
    }

    private void OnDestroy()
    {
        GameActions.PlayerTouchedTheBoard -= PlayBoardTouchSFX;
        GameActions.PlayerStoppedOnItemTile -= PlayGetCurrencySFX;
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
