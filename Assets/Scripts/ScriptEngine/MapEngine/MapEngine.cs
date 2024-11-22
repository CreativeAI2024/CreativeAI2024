using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MapEngine : MonoBehaviour
{
    [SerializeField] private Tilemap colliderTilemap;
    [SerializeField] private Tilemap frontTilemap;
    [SerializeField] private Tilemap middleTilemap;
    [SerializeField] private Tilemap backTilemap;
    [SerializeField] private Tile clearTile;
    public TileMapping tileMapping;
    
    [SerializeField] private MapDataController mapDataController;
    
    public void Initialize()
    {
        Vector2Int mapSize = mapDataController.GetMapSize();
        Dictionary<char, TileBase> tileDictionary = tileMapping.ToDictionary();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                PutBackStyleTile(position, tileDictionary);
                PutMiddleStyleTile(position, tileDictionary);
                PutFrontStyleTile(position, tileDictionary);
                SetCollider(position);
            }
        }
    }
    
    [Conditional("UNITY_EDITOR")]
    private void OnDrawGizmos()
    {
        for (int i = 0; i < mapDataController.GetMapSize().y; i++)
        {
            for (int j = 0; j < mapDataController.GetMapSize().x; j++)
            {
                if (!mapDataController.IsWalkable(new Vector3Int(j, i, 0)))
                {
                    Gizmos.color = Color.white;
                    DrawRect(new Rect(j-0.5f, i-0.5f, 1, 1));
                }
            }
        }
    }
    
    [Conditional("UNITY_EDITOR")]
    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }

    private void SetCollider(Vector3Int position)
    {
        if (!mapDataController.IsWalkable(position))
        {
            colliderTilemap.SetTile(position, clearTile);
        }
    }
    
    private void PutBackStyleTile(Vector3Int position, Dictionary<char, TileBase> tileDictionary)
    {
        PutSingleTile(backTilemap, position, tileDictionary, mapDataController.GetStyleBackChar(position));
    }
    
    private void PutMiddleStyleTile(Vector3Int position, Dictionary<char, TileBase> tileDictionary)
    {
        PutSingleTile(middleTilemap, position, tileDictionary, mapDataController.GetStyleMiddleChar(position));
    }
    
    private void PutFrontStyleTile(Vector3Int position, Dictionary<char, TileBase> tileDictionary)
    {
        PutSingleTile(frontTilemap, position, tileDictionary, mapDataController.GetStyleFrontChar(position));
    }
    
    private void PutSingleTile(Tilemap tilemap, Vector3Int position, Dictionary<char, TileBase> tileDictionary, char tileChar)
    {
        if (tileDictionary.TryGetValue(tileChar, out TileBase tile))
        {
            tilemap.SetTile(position, tile);
        }
    }
}
