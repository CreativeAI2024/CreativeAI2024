using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlagManager : DontDestroySingleton<FlagManager>
{
    private Dictionary<string, bool> _flags;
    private readonly string _flagFilePath = string.Join('/', Application.streamingAssetsPath, "FlagDataExample.json");
    private string _flagSaveFilePath;
    public override void Awake()
    {
        base.Awake();
        _flagSaveFilePath = string.Join('/', Application.persistentDataPath, "FlagData.dat");
        if (File.Exists(_flagSaveFilePath))
        {
            FlagData flags = SaveUtility.SaveFileToData<FlagData>(_flagSaveFilePath);
            _flags = flags.Flags;
        }
        else
        {
            Debug.Log("フラグデータがありませんでした");
            if (File.Exists(_flagFilePath))
            {
                FlagData flags = SaveUtility.JsonToData<FlagData>(_flagFilePath);
                _flags = flags.Flags;
                SaveFlag();
            }
        }
    }
    
    public void AddFlag(string flagName)
    {
        _flags[flagName] = true;
        SaveFlag();
    }
    public void DeleteFlag(string flagName)
    {
        _flags[flagName] = false;
        SaveFlag();
    }
    
    private void SaveFlag()
    {
        FlagData saveFlagData = new FlagData()
        {
            Flags = _flags
        };
        SaveUtility.DataToSaveFile(saveFlagData, _flagSaveFilePath);
    }
    
    public bool HasFlag(string flagName) => _flags[flagName];
}
