using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldManager : MonoBehaviour
{
    public static InputFieldManager Instance;
    
    public List<InputFieldHandler> inputFields;
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }
}
