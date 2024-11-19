using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

[MessagePackObject(true)]
public class ObjectData
{
    public int Id { get; set; }
    public Location[] Location { get; set; }
    public string EventName { get; set; }
    public int TriggerType { get; set; }
    public FlagCondition FlagCondition { get; set; }
}

[MessagePackObject(true)]
public struct Location
{
    public string MapName { get; set; }
    public Vector2Int Position { get; set; }
}

[MessagePackObject(true)]
public struct FlagCondition
{
    public KeyValuePair<string, bool>[] Flag { get; set; }
    public KeyValuePair<string, bool>[] NextFlag { get; set; }
}