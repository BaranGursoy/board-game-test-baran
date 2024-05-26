using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryView inventoryView;

    private int appleCount;
    private int strawberryCount;
    private int pearCount;
    
    private void Awake()
    {
        ActionHandler.SendItemToInventory += AddItemToInventory;
    }

    private void Start()
    {
        inventoryView.UpdateAllInventoryUI(strawberryCount, pearCount, appleCount);
    }

    private void AddItemToInventory(ItemType itemType, int quantity)
    {
        switch (itemType)
        {
            case ItemType.Apple:
                appleCount += quantity;
                inventoryView.UpdateAppleCount(appleCount);
                break;
            case ItemType.Strawberry:
                strawberryCount += quantity;
                inventoryView.UpdateStrawberryCount(strawberryCount);
                break;
            case ItemType.Pear:
                pearCount += quantity;
                inventoryView.UpdatePearCount(pearCount);
                break;
        }
    }
}
