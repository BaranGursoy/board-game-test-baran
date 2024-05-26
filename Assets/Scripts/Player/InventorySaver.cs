using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    public bool SaveExists()
    {
        return PlayerPrefs.HasKey("InventorySave");
    }
    
    public void SaveInventory(Dictionary<ItemType, int> inventoryData)
    {
        DictionarySaver.SaveDictionary("InventorySave", inventoryData);
    }
    
    public void LoadInventory(ref Dictionary<ItemType, int> inventoryData)
    {
        inventoryData = DictionarySaver.LoadDictionary("InventorySave");
    }
    
    public void DeleteInventorySave()
    {
        PlayerPrefs.DeleteKey("InventorySave");
    }
}
