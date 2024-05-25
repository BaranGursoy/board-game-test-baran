using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField] private GameObject emptyTilePrefab;
    [SerializeField] private GameObject cornerTilePrefab;
    [SerializeField] private GameObject appleTilePrefab;
    [SerializeField] private GameObject pearTilePrefab;
    [SerializeField] private GameObject strawberryTilePrefab;

    public float GetTileWidth => emptyTilePrefab.GetComponent<MeshRenderer>().bounds.size.x;

    public MapTile CreateTile(TileData data, Vector3 position, Vector3 rotationVector)
    {
        GameObject tilePrefab = null;

        if (data.isEmpty)
        {
            tilePrefab = emptyTilePrefab;
        }
        
        else if (data.isCorner)
        {
            tilePrefab = cornerTilePrefab;
        }
        
        else
        {
            // Determine the tile type based on the items
            if (data.itemType == ItemType.Apple)
            {
                tilePrefab = appleTilePrefab;
            }
            else if (data.itemType == ItemType.Pear)
            {
                tilePrefab = pearTilePrefab;
            }
            else if (data.itemType == ItemType.Strawberry)
            {
                tilePrefab = strawberryTilePrefab;
            }
        }

        if (tilePrefab != null)
        {
            GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity);
            tileObject.transform.Rotate(rotationVector, Space.Self);
            MapTile mapTile = tileObject.GetComponent<MapTile>();
            mapTile.SetTileData(data);
            return mapTile;
        }

        return null;
    }
}