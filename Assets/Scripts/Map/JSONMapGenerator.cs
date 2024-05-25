using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONMapGenerator : MonoBehaviour, IMapGenerator
{
    private string jsonData;

    public JSONMapGenerator(string jsonData)
    {
        this.jsonData = jsonData;
    }
    
    public void GenerateMap()
    {
        GenerateMapFromJSON(jsonData);
    }

    public List<MapTile> GetTileList()
    {
        return null;
    }

    private void GenerateMapFromJSON(string s)
    {
       //
    }
}
