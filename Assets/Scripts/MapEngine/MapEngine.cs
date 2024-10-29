using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MessagePack;

public class MapEngine : MonoBehaviour
{
    public SpriteRenderer tilePrefab;
    public Dictionary<char, Sprite> spriteDictionary;

    private MapData mapData;

    [SerializeField] private Sprite[] spriteArray; 

    private void Awake()
    {
        InitializeSpriteDictionary();
        LoadMapDataFiles();
    }

    private void InitializeSpriteDictionary()
    {
        spriteDictionary = new Dictionary<char, Sprite>
        {
            { '*', spriteArray[0] }, //黒い余白
            { '#', spriteArray[1] }, //白い壁
            { 'c', spriteArray[2] }, //カーペット
            { 'd', spriteArray[3] }, //ドア
            { '-', spriteArray[4] } //床
        };
    }

    private void LoadMapDataFiles()
    {
        string assetsPath = string.Join('/', Application.streamingAssetsPath, "MapData");
        string[] mapFiles = Directory.GetFiles(assetsPath, "*.json");
        foreach (string filePath in mapFiles)
        {
            LoadMapData(filePath);
        }
    }

    public void LoadMapData(string filePath)
    {
        string jsonContent = File.ReadAllText(filePath);
        byte[] msgPackData = MessagePackSerializer.ConvertFromJson(jsonContent);
        mapData = MessagePackSerializer.Deserialize<MapData>(msgPackData);
        Debug.Log("マップデータを読み込みました: " + filePath);
        PlaceObjects();
    }


    //マップの広さを返す関数
    public Vector2Int GetMapSize()
    {
        return new Vector2Int(mapData.Tiles[0].Length, mapData.Tiles.Length);
    }

    //マップ上にオブジェクトを配置する関数
    public void PlaceObjects()
    {
        Vector2Int mapSize = GetMapSize();

        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                char tileChar = mapData.Tiles[y][x]; // y, xの順に修正
                char styleFrontChar = mapData.StylesFront[y][x]; 
                char styleMiddleChar = mapData.StylesMiddle[y][x]; 
                char styleBackChar = mapData.StylesBack[y][x];

                if (spriteDictionary.TryGetValue(styleBackChar, out Sprite backSprite))
                {
                    SpriteRenderer backTile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                    backTile.sprite = backSprite;
                }
                if (spriteDictionary.TryGetValue(styleMiddleChar, out Sprite middleSprite))
                {
                    SpriteRenderer middleTile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                    middleTile.sprite = middleSprite;
                }
                if (spriteDictionary.TryGetValue(styleFrontChar, out Sprite frontSprite))
                {
                    SpriteRenderer frontTile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                    frontTile.sprite = frontSprite;
                }
                if (spriteDictionary.TryGetValue(tileChar, out Sprite sprite))
                {
                    SpriteRenderer tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                    tile.sprite = sprite;
                }
            }
        }
    }

}