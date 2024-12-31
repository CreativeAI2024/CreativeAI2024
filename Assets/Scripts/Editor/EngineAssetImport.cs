using System.IO;
using UnityEditor;
using UnityEngine;

public class EngineAssetImport
{
    static string streamingAssetsPath = Application.streamingAssetsPath;
    static string resourcesPath = string.Join('/', Application.dataPath, "Resources", "EngineAssets");

    [MenuItem("EngineAssetImport/DeleteEngineAsset", false)]
    public static void DeleteEngineAsset()
    {
        EditorUtility.DisplayProgressBar("Deleting Assets", "Assets deleting Resources folder ...", 0);
        DirectoryInfo directory = new DirectoryInfo(resourcesPath);
        
        foreach (FileInfo file in directory.GetFiles()) {
            file.Delete();
        }
        foreach (DirectoryInfo dir in directory.GetDirectories()) {
            dir.Delete(true);
        }
        
        EditorUtility.DisplayProgressBar("Deleting Assets", "Assets deleting Resources folder ...", 1);
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
        Debug.Log("complete!");
    }
    
    [MenuItem("EngineAssetImport/StreamingAssetsToResources", false)]
    public static void MoveEngineAsset()
    {
        EditorUtility.DisplayProgressBar("Moving Assets", "Assets moving StreamingAssets to Resources folder ...", 0);
        
        CopyDirectory(streamingAssetsPath, resourcesPath, true);
        
        EditorUtility.DisplayProgressBar("Moving Assets", "Assets moving StreamingAssets to Resources folder ...", 1);
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
        Debug.Log("complete!");
    }
    
    /// <summary>
    /// https://learn.microsoft.com/ja-jp/dotnet/standard/io/how-to-copy-directories
    /// </summary>
    static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDir);
        
        // Check if the source directory exists
        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");
        
        // Cache directories before we start copying
        DirectoryInfo[] dirs = dir.GetDirectories();
        
        // Create the destination directory
        Directory.CreateDirectory(destinationDir);
        
        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles())
        {
            if (file.Extension.Equals(".meta"))
            {
                Debug.Log(file.FullName);
                continue;
            }
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath);
        }
        
        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir, true);
            }
        }
    }
}