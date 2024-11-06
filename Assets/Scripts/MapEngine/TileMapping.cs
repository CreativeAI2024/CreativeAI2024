using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileMapping", menuName = "Map/TileMapping")]
public class TileMapping : ScriptableObject
{
    [System.Serializable]
    public struct TileEntry
    {
        //MapDataの中のTileMappingのインスペクターから設定
        public char symbol;
        public TileBase tile;
    }

    public TileEntry[] tileEntries; //構造体

    // 辞書として使用できるように変換するメソッド
    public Dictionary<char, TileBase> ToDictionary()
    {
        var dictionary = new Dictionary<char, TileBase>();
        foreach (var entry in tileEntries)
        {
            dictionary[entry.symbol] = entry.tile;
        }
        return dictionary;
    }
}
