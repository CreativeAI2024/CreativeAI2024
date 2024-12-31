using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StreamingAssetLoader : IFileAssetLoader
{
    public string[] GetPathDirectory(string path)
    {
        return Directory.GetFiles(path);
    }
    
    public string GetPath(string path)
    {
        return $"{Application.streamingAssetsPath}/{path}";
    }
    
    public string LoadFileAsset(string assetPath)
    {
        return File.ReadAllText(assetPath);
    }
}
