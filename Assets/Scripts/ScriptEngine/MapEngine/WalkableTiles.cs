using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkableTiles", menuName = "Map/WalkableTiles")]
public class WalkableTiles : ScriptableObject
{
    [SerializeField] private List<char> walkableTileSymbols;
    
    public bool IsWalkable(char tileChar)
    {
        return walkableTileSymbols.Contains(tileChar);
    }
}