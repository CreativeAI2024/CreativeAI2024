using System.Collections.Generic;
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
