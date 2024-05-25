using System.Collections.Generic;
using UnityEngine;

public class RandomMapGenerator : IMapGenerator
{
    [SerializeField] private List<MapTile> tileList = new List<MapTile>();

    private int _xDimension;
    private int _zDimension;

    private float _tileWidth;
    private Transform _tileStartPoint;
    private Transform _tileParentTransform;
    
    private TileFactory _tileFactory;

    public RandomMapGenerator(int xDimension, int zDimension, TileFactory tileFactory, float tileWidth,
        Transform tileStartPoint, Transform tileParentTransform)
    {
        this._xDimension = xDimension;
        this._zDimension = zDimension;
        this._tileFactory = tileFactory;
        this._tileWidth = tileWidth;
        this._tileStartPoint = tileStartPoint;
        this._tileParentTransform = tileParentTransform;
    }

    public void GenerateMap()
    {
        GenerateRandomMap(_xDimension, _zDimension);
    }
    
    public List<MapTile> GetTileList()
    {
        return tileList;
    }

    private void GenerateRandomMap(int xDimension, int zDimension)
    {
        Direction currentDirection = Direction.Left;
        
        if (tileList.Count == 0) // Starting corner tile
        {
            TileData startCornerTileData = CreateCornerTileData();
            MapTile firstCreatedTile = _tileFactory.CreateTile(startCornerTileData, _tileStartPoint.position, Vector3.zero, _tileParentTransform);
            tileList.Add(firstCreatedTile);
        }
        
        for (int i = 0; i < 4; i++)
        {
            int currentDimension = i % 2 == 0 ? xDimension : zDimension;
            Vector3 currentRotationVector = Vector3.zero;
            
            switch (i)
            {
                case 0 :
                    currentDirection = Direction.Left;
                    break;
                case 1 :
                    currentDirection = Direction.Up;
                    currentRotationVector = new Vector3(0, 90f, 0f);
                    break;
                case 2 :
                    currentDirection = Direction.Right;
                    currentRotationVector = new Vector3(0, 180f, 0f);
                    break;
                case 3 :
                    currentDirection = Direction.Down;
                    currentRotationVector = new Vector3(0, -90f, 0f);
                    break;
            }
            
            for (int j = 0; j < currentDimension; j++)
            {
                TileData tileData = CreateRandomTileData();

                
                MapTile createdTile = _tileFactory.CreateTile(tileData, NextPosition(currentDirection), currentRotationVector, _tileParentTransform);
                tileList.Add(createdTile);
            }

            if (i < 3)
            {
                TileData cornerTileData = CreateCornerTileData();
                MapTile cornerTile = _tileFactory.CreateTile(cornerTileData, NextPosition(currentDirection, isCornerTile:true), currentRotationVector, _tileParentTransform);
                tileList.Add(cornerTile);
            }
        }
        
        TileManager.MapGenerationFinished?.Invoke();
    }

    private Vector3 NextPosition(Direction currentDirection, bool isCornerTile = false)
    {
        Vector3 nextPosition;

        float factor = tileList[^1] is CornerTile ? 1.33f : 1.0f;

        if (isCornerTile) factor = 1.33f;

        switch (currentDirection)
        {
            case Direction.Left:
                nextPosition = tileList[^1].transform.position + (_tileWidth * factor * Vector3.left);
                break;
            case Direction.Up:
                nextPosition = tileList[^1].transform.position + (_tileWidth * factor * Vector3.forward);
                break;
            case Direction.Right:
                nextPosition = tileList[^1].transform.position + (_tileWidth * factor * Vector3.right);
                break;
            case Direction.Down:
                nextPosition = tileList[^1].transform.position + (_tileWidth * factor * Vector3.back);
                break;
            default:
                nextPosition = Vector3.zero;
                break;
        }

        return nextPosition;
    }

    private TileData CreateRandomTileData()
    {
        TileData tileData = new TileData();
        int randomValue = Random.Range(0, 5);
        if (randomValue == 0)
        {
            tileData.isEmpty = true;
        }
        else
        {
            tileData.isEmpty = false;
            ItemType itemType = (ItemType)Random.Range(0, 3);
            tileData.itemType = itemType;
            int itemCount = Random.Range(1, 20);
            tileData.quantity = itemCount;
        }
        return tileData;
    }
    
    private TileData CreateCornerTileData()
    {
        TileData tileData = new TileData
        {
            isCorner = true
        };

        return tileData;
    }

}

public enum Direction
{
    Left,
    Up,
    Right,
    Down
}