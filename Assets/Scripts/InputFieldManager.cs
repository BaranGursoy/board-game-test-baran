using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldManager : MonoBehaviour
{
    public static InputFieldManager Instance;
    
    public List<InputFieldHandler> inputFields;

    [SerializeField] private GameObject inputFieldPrefab;
    
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

    public void CreateInputField()
    {
        var createdInputField = Instantiate(inputFieldPrefab, transform).GetComponent<InputFieldHandler>();
        createdInputField.SetPlaceHolderName(index: inputFields.Count);
        inputFields.Add(createdInputField);
    }
}
