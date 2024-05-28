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
       //This can be extendable to JSON version, I did a random map generation, but since I used an interface JSON version can be implemented easily.
    }
}
