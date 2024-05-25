using UnityEngine;

public class PearTile : MapTile
{
    public override void SetTileData(TileData data)
    {
        quantityTMP.text = $"x{data.quantity}";
    }
}