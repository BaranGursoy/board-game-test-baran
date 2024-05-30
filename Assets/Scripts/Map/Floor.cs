using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private bool firstDiceTouchedTheFloor = false;

    private void Awake()
    {
        GameActions.DiceTouchedTheFloor += SetFirstDiceTouchedTheFloor;
        GameActions.AllDicesStopped += ResetFirstDiceTouchedTheFloor;
    }

    private void OnDestroy()
    {
        GameActions.DiceTouchedTheFloor -= SetFirstDiceTouchedTheFloor;
        GameActions.AllDicesStopped -= ResetFirstDiceTouchedTheFloor;
    }

    private void ResetFirstDiceTouchedTheFloor()
    {
        firstDiceTouchedTheFloor = false;
    }

    private void SetFirstDiceTouchedTheFloor()
    {
        if (firstDiceTouchedTheFloor) return;
            
        firstDiceTouchedTheFloor = true;
        GameActions.FirstDiceTouchedTheFloor?.Invoke();
    }
}
