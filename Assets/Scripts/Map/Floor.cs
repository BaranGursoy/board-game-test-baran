using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private bool firstDiceTouchedTheFloor = false;

    private void Awake()
    {
        ActionHandler.DiceTouchedTheFloor += SetFirstDiceTouchedTheFloor;
        ActionHandler.AllDicesStopped += ResetFirstDiceTouchedTheFloor;
    }

    private void ResetFirstDiceTouchedTheFloor()
    {
        firstDiceTouchedTheFloor = false;
    }

    private void SetFirstDiceTouchedTheFloor()
    {
        if (firstDiceTouchedTheFloor) return;
            
        firstDiceTouchedTheFloor = true;
        ActionHandler.FirstDiceTouchedTheFloor?.Invoke();
    }
}
