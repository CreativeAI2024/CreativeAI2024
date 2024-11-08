using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesFileAssetLoader : IFileAssetLoader
{
    public string LoadFileAsset(string assetPath)
    {
        string relativePath = Path.GetRelativePath(Application.dataPath, assetPath);
        TextAsset textAsset = Resources.Load<TextAsset>(relativePath);
        return textAsset.text;
    }
}
