using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private InventorySaver inventorySaver;

    private Dictionary<ItemType, int> inventoryData = new();

    private void Awake()
    {
        ActionHandler.SendItemToInventory += AddItemToInventory;
    }

    private void Start()
    {
        InitializeInventory();
        inventoryView.UpdateAllInventoryUI(inventoryData);
    }

    private void InitializeInventory()
    {
        if (inventorySaver.SaveExists())
        {
            inventorySaver.LoadInventory(ref inventoryData);
        }
        else
        {
            ResetInventory();
        }
    }

    private void AddItemToInventory(ItemType itemType, int quantity)
    {
        inventoryData[itemType] += quantity;
        inventoryView.UpdateQuantityOfItem(itemType, inventoryData[itemType]);

        inventorySaver.SaveInventory(inventoryData);
    }

    private void ResetInventory()
    {
        inventorySaver.DeleteInventorySave();
        inventoryData.Clear();
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            inventoryData.Add(itemType, 0);
        }
    }
}