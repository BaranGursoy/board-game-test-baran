using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private ItemTextPair[] itemTextPair;
    
    public void UpdateQuantityOfItem(ItemType itemType, int quantity)
    {
        ItemTextPair itemToUpdate = Array.Find(itemTextPair, x => x.itemType == itemType);
        itemToUpdate.itemQuantityTMP.text = $"x{quantity}";
    }

    public void UpdateAllInventoryUI(Dictionary<ItemType, int> itemDict)
    {
        foreach (ItemTextPair itemText in itemTextPair)
        {
            if(!itemDict.ContainsKey(itemText.itemType)) continue;
            
            itemText.itemQuantityTMP.text = $"x{itemDict[itemText.itemType]}";
        }
    }
}

[Serializable]
public struct ItemTextPair
{
    public ItemType itemType;
    public TextMeshProUGUI itemQuantityTMP;
}

