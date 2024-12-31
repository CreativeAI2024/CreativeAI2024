using System.IO;
using System.Linq;
using UnityEngine;

public class ResourcesFileAssetLoader : IFileAssetLoader
{
    public string[] GetPathDirectory(string path)
    {
        TextAsset[] textAsset = Resources.LoadAll<TextAsset>(path);
        return textAsset.Select(x => string.Join('/', path, x.name)).ToArray();
    }
    
    public string GetPath(string path)
    {
        string resourcePath = string.Join('/', "EngineAssets", path);
        if (Path.HasExtension(resourcePath))
        {
            return resourcePath.Replace(new FileInfo(resourcePath).Extension, string.Empty);
        }
        return resourcePath;
    }
    
    public string LoadFileAsset(string assetPath)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(assetPath);
        return textAsset.text;
    }
}
