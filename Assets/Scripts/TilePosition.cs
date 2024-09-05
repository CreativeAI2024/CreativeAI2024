using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosition : MonoBehaviour
{
    public Vector2 playerPositionStartDiff;
    public Vector2Int GridPosition => new Vector2Int((int)(transform.position.x + playerPositionStartDiff.x), (int)(transform.position.y + playerPositionStartDiff.y));
}
