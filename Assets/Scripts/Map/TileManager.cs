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
    
    [SerializeField][Min(3)] private int xDimensionTileCount;
    [SerializeField][Min(3)] private int zDimensionTileCount;
    [SerializeField] private TileFactory tileFactory;
    [SerializeField] private Transform tileStartPoint;
    [SerializeField] private Transform floorTransform;
    [SerializeField] private Transform sealingTransform;

    private float scaleFactorForFloorAndSealingX = 1f;
    private float scaleFactorForFloorAndSealingZ = 1f;

    public IMapGenerator mapGenerator;

    private void Start()
    {
        mapGenerator = new RandomMapGenerator(xDimensionTileCount, zDimensionTileCount, tileFactory, tileFactory.GetTileWidth, tileStartPoint, transform);
        mapGenerator.GenerateMap();
        ScaleFloorAndSealing();
    }

    private void ScaleFloorAndSealing()
    {
        float ratioX = xDimensionTileCount / 25f;

        if (ratioX > 1f)
        {
            scaleFactorForFloorAndSealingX = ratioX;
        }
        
        float ratioZ = zDimensionTileCount / 25f;
        
        if (ratioZ > 1f)
        {
            scaleFactorForFloorAndSealingZ = ratioZ;
        }

        Vector3 newScaleForFloorAndSealing = new Vector3(floorTransform.localScale.x * scaleFactorForFloorAndSealingX,
            floorTransform.localScale.y, floorTransform.localScale.z * scaleFactorForFloorAndSealingZ);

        floorTransform.localScale = newScaleForFloorAndSealing;
        sealingTransform.localScale = newScaleForFloorAndSealing;
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