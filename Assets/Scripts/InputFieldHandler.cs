using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class InputFieldHandler : MonoBehaviour
{
    private int diceValue = 1;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI inputFieldPlaceHolderTMP;
    public void LimitInputFieldValue(string inputFieldText)
    {
        if (inputFieldText is "-" or "+")
        {
            inputField.text = string.Empty;
        }
        
        if (int.TryParse(inputFieldText, out int value))
        {
            if (value <= 0)
            {
                value = 1;
            }
            else if (value > 6)
            {
                value = 6;
            }

            inputField.text = value.ToString();
        }

        else
        {
            diceValue = Random.Range(1, 7);
        }

        diceValue = value;
    }

    public int GetDiceValue()
    {
        if (String.IsNullOrEmpty(inputField.text))
        {
            diceValue = Random.Range(1, 7);
        }

        return diceValue;
    }

    public void SetPlaceHolderName(int index)
    {
        string placeHolderText = $"Dice {index + 1} Value";
        inputFieldPlaceHolderTMP.text = placeHolderText;
    }
}
