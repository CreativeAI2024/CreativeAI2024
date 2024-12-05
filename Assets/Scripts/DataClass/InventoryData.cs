using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

[MessagePackObject(true)]
public class InventoryData
{
    public List<string> Items { get; set; }
}