using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI strawberryTMP;
    [SerializeField] private TextMeshProUGUI pearTMP;
    [SerializeField] private TextMeshProUGUI appleTMP;

    public void UpdateStrawberryCount(int strawberryCount)
    {
        strawberryTMP.text = $"x{strawberryCount}";
    }
    
    public void UpdatePearCount(int pearCount)
    {
        pearTMP.text = $"x{pearCount}";
    }
    
    public void UpdateAppleCount(int appleCount)
    {
        appleTMP.text = $"x{appleCount}";
    }

    public void UpdateAllInventoryUI(int strawberryCount, int pearCount, int appleCount)
    {
        strawberryTMP.text = $"x{strawberryCount}";
        pearTMP.text = $"x{pearCount}";
        appleTMP.text = $"x{appleCount}";
    }
}
