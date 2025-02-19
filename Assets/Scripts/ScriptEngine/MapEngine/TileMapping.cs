using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileMapping", menuName = "Map/TileMapping")]
public class TileMapping : ScriptableObject
{
    [System.Serializable]
    public struct TileEntry
    {
        public char symbol;
        public TileBase tile;
    }
    
    [SerializeField] private TileEntry[] tileEntries;
    
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
