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
        
        for (int i = 0; i < _dropdownMenu.value + 1; i++)
        {
            var createdInputField = Instantiate(inputFieldPrefab, transform).GetComponent<InputFieldHandler>();
            createdInputField.SetPlaceHolderName(index: inputFields.Count);
            inputFields.Add(createdInputField);
        }
      
    }
}
