using System.Collections.Generic;
using UnityEngine;

public abstract class MapTile : MonoBehaviour
{
    public abstract void SetTileData(TileData data);
}

public class TileData
{
    public bool IsEmpty { get; set; }
    public Dictionary<ItemType, int> Items { get; set; }
}

public enum ItemType
{
    Apple,
    Pear,
    Strawberry
}