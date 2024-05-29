using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceButton : MonoBehaviour
{
    private void Awake()
    {
        GameActions.HideDiceButton += HideButton;
        GameActions.PlayerStopped += ShowButton;
    }

    public void ShowButton()
    {
        gameObject.SetActive(true);
    }
    
    public void HideButton()
    {
        gameObject.SetActive(false);
    }
}
