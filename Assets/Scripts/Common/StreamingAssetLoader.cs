using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StreamingAssetLoader : IFileAssetLoader
{
    public string LoadFileAsset(string assetPath)
    {
        return File.ReadAllText(assetPath);
    }
}
