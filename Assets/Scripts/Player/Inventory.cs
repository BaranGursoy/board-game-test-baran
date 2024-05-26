using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private void Awake()
    {
        ActionHandler.SendItemToInventory += Test;
    }

    private void Test(ItemType itemType, int quantity)
    {
        Debug.Log($"Gained {Enum.GetName(typeof(ItemType),itemType)}, x{quantity}!");
    }
}
