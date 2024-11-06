using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;

public class MapEngine : MonoBehaviour
{
    public Tilemap tilemap;
    public TileMapping tileMapping;

    private Dictionary<char, TileBase> tileDictionary;
    private MapData mapData;

    private void Awake()
    {
        InitializeTileDictionary();
        LoadMapDataFiles();
    }

    private void InitializeTileDictionary()
    {
        tileDictionary = tileMapping.ToDictionary();
    }

    private void LoadMapDataFiles()
    {
        string assetsPath = Path.Combine(Application.streamingAssetsPath, "MapData");
        string[] mapFiles = Directory.GetFiles(assetsPath, "*.json");
        foreach (string filePath in mapFiles)
        {
            LoadMapData(filePath);
        }
    }

    public void LoadMapData(string filePath)
    {
        mapData = SaveUtility.JsonToData<MapData>(filePath);
        PlaceTiles();
    }

    public void PlaceTiles()
    {
        Vector2Int mapSize = GetMapSize();

        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                char tileChar = mapData.Tiles[y][x];
                Vector3Int position = new Vector3Int(x, -y, 0);

                PlaceSingleTile(position, "Back");
                PlaceSingleTile(position, "Middle");
                PlaceSingleTile(position, "Front");
                SetCollider(position);
            }
        }
    }

    private void SetCollider(Vector3Int position)
    {
        char tileChar = mapData.Tiles[-position.y][position.x];

        if (tilemap.HasTile(position))
        {
            if (tileMapping.IsWalkable(tileChar))
            {
                tilemap.SetColliderType(position, Tile.ColliderType.None);
                DebugLogger.Log($"通行可能: {-position.y}{position.x}");
            }
            else
            {
                tilemap.SetColliderType(position, Tile.ColliderType.Grid);
                DebugLogger.Log($"通行不可: {-position.y}{position.x}");
            }
        }
    }

    private void PlaceSingleTile(Vector3Int position, string styleType)
    {
        char tileChar = mapData.Tiles[-position.y][position.x];
        if (tileDictionary.TryGetValue(tileChar, out TileBase tile))
        {
            tilemap.SetTile(position, tile);
            DebugLogger.Log($"{styleType}スタイルタイル '{tileChar}' が配置されました: (position)");
        }
    }

    public Vector2Int GetMapSize()
    {
        return new Vector2Int(mapData.Tiles[0].Length, mapData.Tiles.Length);
    }
}
