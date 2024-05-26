using System;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }

    [SerializeField] private int xDimensionTileCount;
    [SerializeField] private int zDimensionTileCount;
    [SerializeField] private TileFactory tileFactory;
    [SerializeField] private Transform tileStartPoint;

    public IMapGenerator mapGenerator;

    private void Start()
    {
        mapGenerator = new RandomMapGenerator(xDimensionTileCount, zDimensionTileCount, tileFactory, tileFactory.GetTileWidth, tileStartPoint, transform);
        mapGenerator.GenerateMap();
    }
    
    public Vector3 CalculateMiddlePoint()
    {
        // Get all the tiles in the map
        List<MapTile> tileList = mapGenerator.GetTileList();

        // If there are no tiles, return Vector3.zero
        if (tileList.Count == 0)
        {
            return Vector3.zero;
        }

        // Calculate the average position of all the tiles
        Vector3 sumPosition = Vector3.zero;
        foreach (MapTile tile in tileList)
        {
            sumPosition += tile.transform.position;
        }

        return sumPosition / tileList.Count;
    }

    /*public void GenerateMapFromJSON(string jsonData)
    {
        mapGenerator = new JSONMapGenerator(jsonData, tileFactory);
        mapGenerator.GenerateMap();
    }*/
}