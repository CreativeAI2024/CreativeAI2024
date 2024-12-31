using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFileAssetLoader
{
    public string[] GetPathDirectory(string path);
    public string GetPath(string path);
    public string LoadFileAsset(string assetPath);
}
