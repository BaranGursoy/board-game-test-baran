using System;
using UnityEngine;

public class AppleTile : MapTile
{
    public override void SetTileData(TileData data)
    {
        quantityTMP.text = $"x{data.quantity}";
    }
}