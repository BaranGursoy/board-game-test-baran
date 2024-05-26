using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MapTile : MonoBehaviour
{
    protected TextMeshPro quantityTMP;
    protected TileData tileData;

    public abstract void StoppedOnTile();

    protected ItemType GetItemFromTile()
    {
        return tileData.itemType;
    }
    
    protected int GetItemQuantity()
    {
        return tileData.quantity;
    }

    protected void ItemSendCommonSteps()
    {
        ActionHandler.SendItemToInventory?.Invoke(GetItemFromTile(), GetItemQuantity());
        ActionHandler.SpawnParticles?.Invoke(GetItemFromTile(), transform.position + (transform.up / 2f));
    }

    public void SetTileData(TileData data)
    {
        tileData = data;
        UpdateTileText();
    }

    protected void UpdateTileText()
    {
        if (!quantityTMP) return;
        quantityTMP.text = $"x{tileData.quantity}";
    }

    protected void Awake()
    {
        quantityTMP = GetComponentInChildren<TextMeshPro>();
    }
}

public class TileData
{
    public bool isEmpty;
    public bool isCorner;
    public ItemType itemType;
    public int quantity;
}

public enum ItemType
{
    Apple,
    Pear,
    Strawberry
}