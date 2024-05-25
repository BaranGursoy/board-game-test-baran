using System.Collections.Generic;

public interface IMapGenerator
{
    void GenerateMap();
    List<MapTile> GetTileList();
}
