using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

[MessagePackObject(true)]
public class FlagData
{
    public Dictionary<string, bool> Flags { get; set; }
}