using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TileInfo : MonoBehaviour
{
    public Vector2 positionStartDiff;
    public Vector2Int GridPosition => new Vector2Int((int)(transform.position.x + positionStartDiff.x), (int)(transform.position.y + positionStartDiff.y));
    public Vector2Int Direction => new Vector2Int(Mathf.RoundToInt(Mathf.Sin(transform.rotation.eulerAngles.z)), Mathf.RoundToInt(Mathf.Cos(transform.rotation.eulerAngles.z)));
}
