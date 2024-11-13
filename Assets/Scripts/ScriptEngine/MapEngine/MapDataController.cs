using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class MapDataController : MonoBehaviour
{
    private MapData mapData;
    public WalkableTiles walkableTiles;
    
    public void LoadMapData(string fileName)
    {
        string assetsPath = Path.Combine(Application.streamingAssetsPath, "MapData");
        string[] mapFiles = Directory.GetFiles(assetsPath, "*.json");
        var filePath = mapFiles.FirstOrDefault(x => x.EndsWith($"{fileName}.json"));
        mapData = SaveUtility.JsonToData<MapData>(filePath);
    }
    
    public bool IsWalkable(Vector3Int position)
    {
        char tileChar = mapData.Tiles[GetMapSize().y-1-position.y][position.x];
        return walkableTiles.IsWalkable(tileChar);
    }
    
    public char GetStyleBackChar(Vector3Int position)
    {
        return mapData.StylesBack[GetMapSize().y-1-position.y][position.x];
    }
    
    public char GetStyleMiddleChar(Vector3Int position)
    {
        return mapData.StylesMiddle[GetMapSize().y-1-position.y][position.x];
    }
    
    public char GetStyleFrontChar(Vector3Int position)
    {
        return mapData.StylesFront[GetMapSize().y-1-position.y][position.x];
    }
    
    public Vector2Int GetMapSize()
    {
        return new Vector2Int(mapData.Tiles[0].Length, mapData.Tiles.Length);
    }
    
    public Vector2Int ConvertGridPosition(Vector2Int gridPosition)
    {
        int clampedXPos = Mathf.Clamp(gridPosition.x, 0, GetMapSize().x - 1);
        int clampedYPos = Mathf.Clamp(gridPosition.y, 0, GetMapSize().y - 1);
        return new Vector2Int(clampedXPos, clampedYPos);
    }
}
