using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MapDataController : MonoBehaviour
{
    public enum TileLayer
    {
        Tiles,
        StylesFront,
        StylesMiddle,
        StylesBack
    }
    
    private MapData mapData;
    public WalkableTiles walkableTiles;
    private Action _resetAction;
    private static Dictionary<string, Queue<(TileLayer, Vector2Int, char)>> mapDictionary = new Dictionary<string, Queue<(TileLayer, Vector2Int, char)>>();
    private string _mapName;
    public void SetChange(Action reset)
    {
        _resetAction = reset;
    }
    
    public void ApplyMapChange()
    {
        _resetAction?.Invoke();
    }
    
    public void LoadMapData(string mapName)
    {
        _mapName = mapName;
        string assetsPath = Path.Combine(Application.streamingAssetsPath, "MapData");
        string[] mapFiles = Directory.GetFiles(assetsPath, "*.json");
        var filePath = mapFiles.FirstOrDefault(x => x.EndsWith($"{mapName}.json"));
        mapData = SaveUtility.JsonToData<MapData>(filePath);
        if (!mapDictionary.ContainsKey(mapName))
        {
            mapDictionary.Add(mapName, new Queue<(TileLayer, Vector2Int, char)>());
        }
        else
        {
            foreach ((TileLayer, Vector2Int, char) position in mapDictionary[mapName])
            {
                ChangeMapTile(mapName, position.Item1, position.Item2, position.Item3);
            }
            ApplyMapChange();
        }
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
    
    public void ChangeMapTile(string mapName, TileLayer layer, Vector2Int position, char tipSign)
    {
        if (_mapName.Equals(mapName))
        {
            switch (layer)
            {
                case TileLayer.Tiles:
                    mapData.Tiles[position.x] = GetChangeTileString(position, tipSign);
                    break;
                case TileLayer.StylesFront:
                    mapData.StylesFront[position.x] = GetChangeTileString(position, tipSign);
                    break;
                case TileLayer.StylesMiddle:
                    mapData.StylesMiddle[position.x] = GetChangeTileString(position, tipSign);
                    break;
                case TileLayer.StylesBack:
                    mapData.StylesBack[position.x] = GetChangeTileString(position, tipSign);
                    break;
            }
        }
        if (!mapDictionary.ContainsKey(mapName))
        {
            mapDictionary.Add(mapName, new Queue<(TileLayer, Vector2Int, char)>());
        }
        mapDictionary[mapName].Enqueue((layer, position, tipSign));
    }
    
    private string GetChangeTileString(Vector2Int position, char tipSign)
    {
        char[] charArray = mapData.Tiles[position.x].ToCharArray();
        charArray[position.y] = tipSign;
        return charArray.ToString();
    }
    
    public Vector2Int GetMapSize()
    {
        return new Vector2Int(mapData.Tiles[0].Length, mapData.Tiles.Length);
    }
    
    public bool IsGridPositionOutOfRange(Vector2Int gridPosition)
    {
        return gridPosition.x < 0 || GetMapSize().x <= gridPosition.x || 
               gridPosition.y < 0 || GetMapSize().y <= gridPosition.y;
    }
    
    public Vector2Int ConvertGridPosition(Vector2Int gridPosition)
    {
        int clampedXPos = Mathf.Clamp(gridPosition.x, 0, GetMapSize().x - 1);
        int clampedYPos = Mathf.Clamp(gridPosition.y, 0, GetMapSize().y - 1);
        return new Vector2Int(clampedXPos, clampedYPos);
    }
}
