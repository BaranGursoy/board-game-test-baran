using UnityEngine;

public class RandomMapGenerator : IMapGenerator
{
    private int width;
    private int height;
    private TileFactory tileFactory;
    private Transform parentTransform; // Parent transform for organizing tiles in the hierarchy

    public RandomMapGenerator(int width, int height, TileFactory tileFactory, Transform parentTransform)
    {
        this.width = width;
        this.height = height;
        this.tileFactory = tileFactory;
        this.parentTransform = parentTransform;
    }

    public void GenerateMap()
    {
        GenerateRandomMap(width, height);
    }

    private void GenerateRandomMap(int width, int height)
    {
        // Generate random map data
        // For each tile data, create a tile using the tileFactory
        // Position tiles in a grid

        // Example pseudocode:
        // for (int x = 0; x < width; x++)
        // {
        //     for (int y = 0; y < height; y++)
        //     {
        //         TileData tileData = CreateRandomTileData();
        //         Vector3 position = new Vector3(x, 0, y);
        //         tileFactory.CreateTile(tileData, position);
        //     }
        // }
    }
}