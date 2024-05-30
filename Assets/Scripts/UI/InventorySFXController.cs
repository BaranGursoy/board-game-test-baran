using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySFXController : MonoBehaviour
{
    [SerializeField] private AudioSource currencyGainedAudioSource;
    private void Awake()
    {
        GameActions.AllCurrenciesReachedInventory += PlayCurrencyGainedSFX;
    }

    private void OnDestroy()
    {
        GameActions.AllCurrenciesReachedInventory -= PlayCurrencyGainedSFX;
    }

    private void PlayCurrencyGainedSFX()
    {
        currencyGainedAudioSource.Play();
    }
}
