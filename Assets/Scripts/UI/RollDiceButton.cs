using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceButton : MonoBehaviour
{
    private void Awake()
    {
        ActionHandler.HideDiceButton += HideButton;
        ActionHandler.PlayerStopped += ShowButton;
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
