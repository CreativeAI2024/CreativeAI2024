using System.IO;
using UnityEngine;
using UnityEditor;

public class DeleteFlagDataCache
{
    [MenuItem("FlagData/DeleteFlagDataCache", false)]
    public static void DeleteFlagData()
    {
        string flagDataPath = string.Join('/', Application.persistentDataPath, "FlagData.dat");
        if (File.Exists(flagDataPath))
        {
            File.Delete(flagDataPath);
            Debug.Log("Success Delete FlagData.dat");
        }
        else
        {
            Debug.Log("Does Not Exists FlagData.dat");
        }
    }
}