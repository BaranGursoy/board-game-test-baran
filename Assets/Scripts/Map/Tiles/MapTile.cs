using TMPro;
using UnityEngine;

public abstract class MapTile : MonoBehaviour
{
    [SerializeField] private TextMeshPro quantityTMP;
    [SerializeField] private TextMeshPro tileNumberTMP;
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
        ActionHandler.PlayerStoppedOnItemTile?.Invoke();
        ActionHandler.SpawnCurrency?.Invoke(GetItemFromTile(), transform.position + (transform.up / 2f), GetItemQuantity());
    }

    public void SetTileData(TileData data)
    {
        tileData = data;
        UpdateTileText();
    }

    public void SetTileNumber(int tileNumber)
    {
        tileNumberTMP.text = tileNumber.ToString();
    }

    protected void UpdateTileText()
    {
        if (!quantityTMP) return;
        quantityTMP.text = $"x{tileData.quantity}";
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