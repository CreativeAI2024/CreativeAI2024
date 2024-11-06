using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;

public class MapEngine : MonoBehaviour
{
    public Tilemap tilemap; // TilemapをInspectorから指定
    public TileMapping tileMapping; // ScriptableObjectのTileMappingをInspectorから指定

    private Dictionary<char, TileBase> tileDictionary; // タイル辞書
    private MapData mapData;

    private void Awake()
    {
        InitializeTileDictionary();
        LoadMapDataFiles();
    }

    private void InitializeTileDictionary()
    {
        // ScriptableObjectから辞書を生成
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

                PlaceSingleTile(mapData.StylesBack[y][x], x, y, "Back");
                PlaceSingleTile(mapData.StylesMiddle[y][x], x, y, "Middle");
                PlaceSingleTile(mapData.StylesFront[y][x], x, y, "Front");

                SetCollider(tileChar, x, y);
            }
        }
    }

    private void SetCollider(char tileChar, int x, int y)
    {
        Vector3Int position = new Vector3Int(x, -y, 0);

        if (tilemap.HasTile(position))
        {
            if (tileChar == 'c' || tileChar == 'd' || tileChar == '-')
            {
                tilemap.SetColliderType(position, Tile.ColliderType.None);
                Debug.Log($"通行可能: ({x}, {-y})");
            }
            else
            {
                tilemap.SetColliderType(position, Tile.ColliderType.Grid);
                Debug.Log($"通行不可: ({x}, {-y})");
            }
        }
    }

    private void PlaceSingleTile(char tileChar, int x, int y, string styleType)
    {
        if (tileDictionary.TryGetValue(tileChar, out TileBase tile))
        {
            tilemap.SetTile(new Vector3Int(x, -y, 0), tile);
            Debug.Log($"{styleType}スタイルタイル '{tileChar}' が配置されました: ({x}, {-y})");
        }
    }

    public Vector2Int GetMapSize()
    {
        return new Vector2Int(mapData.Tiles[0].Length, mapData.Tiles.Length);
    }
}
