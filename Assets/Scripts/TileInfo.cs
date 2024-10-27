using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TileInfo : MonoBehaviour
{
    [FormerlySerializedAs("playerPositionStartDiff")] public Vector2 positionStartDiff;
    public Vector2Int GridPosition => new Vector2Int((int)(transform.position.x + positionStartDiff.x), (int)(transform.position.y + positionStartDiff.y));
    public int Direction => Mathf.FloorToInt(transform.rotation.eulerAngles.z / 45);
}
