using System;
using UnityEngine;

public class TileManager : MonoBehaviour
{
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

    /*public void GenerateMapFromJSON(string jsonData)
    {
        mapGenerator = new JSONMapGenerator(jsonData, tileFactory);
        mapGenerator.GenerateMap();
    }*/
}