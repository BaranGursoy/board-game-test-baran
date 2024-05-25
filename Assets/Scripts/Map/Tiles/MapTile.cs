using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MapTile : MonoBehaviour
{
    protected TextMeshPro quantityTMP;
    public abstract void SetTileData(TileData data);

    private void Awake()
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