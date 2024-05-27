using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFieldManager : MonoBehaviour
{
    public static InputFieldManager Instance;
    
    public List<InputFieldHandler> inputFields;

    [SerializeField] private TMP_Dropdown _dropdownMenu;

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

    public void CreateInputField(int dummy)
    {
        foreach (var inputField in inputFields)
        {
            Destroy(inputField.gameObject);
        }
        
        inputFields.Clear();

        if (_dropdownMenu.value == 0)
        {
            CreateInputField();
        }
        
        for (int i = 0; i < _dropdownMenu.value; i++)
        {
            CreateInputField();
        }
      
    }

    private void CreateInputField()
    {
        var createdInputField = Instantiate(inputFieldPrefab, transform).GetComponent<InputFieldHandler>();
        createdInputField.SetPlaceHolderName(index: inputFields.Count);
        inputFields.Add(createdInputField);
    }
}
