using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField] private GameObject emptyTilePrefab;
    [SerializeField] private GameObject appleTilePrefab;
    [SerializeField] private GameObject pearTilePrefab;
    [SerializeField] private GameObject strawberryTilePrefab;

    public MapTile CreateTile(TileData data, Vector3 position)
    {
        GameObject tilePrefab = null;

        if (data.IsEmpty)
        {
            tilePrefab = emptyTilePrefab;
        }
        else
        {
            // Determine the tile type based on the items
            if (data.Items.ContainsKey(ItemType.Apple))
            {
                tilePrefab = appleTilePrefab;
            }
            else if (data.Items.ContainsKey(ItemType.Pear))
            {
                tilePrefab = pearTilePrefab;
            }
            else if (data.Items.ContainsKey(ItemType.Strawberry))
            {
                tilePrefab = strawberryTilePrefab;
            }
        }

        if (tilePrefab != null)
        {
            GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity);
            MapTile mapTile = tileObject.GetComponent<MapTile>();
            mapTile.SetTileData(data);
            return mapTile;
        }

        return null;
    }
}