using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

[MessagePackObject(true)]
public class MapData
{
    public string[] Tiles { get; set; }
    public string[] StylesFront { get; set; }
    public string[] StylesMiddle { get; set; }
    public string[] StylesBack { get; set; }
}
