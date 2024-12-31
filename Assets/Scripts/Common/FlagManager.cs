using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlagManager : DontDestroySingleton<FlagManager>
{
    private Dictionary<string, bool> _flags;
    public int ReiStatus { get; private set; }
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
            SaveInitFlags();
        }
        ReiStatus = PlayerPrefs.GetInt("ReiStatus",0);
    }
    
    private void SaveInitFlags()
    {
        IFileAssetLoader loader = SaveUtility.FileAssetLoaderFactory();
        string flagFilePath = loader.GetPath("FlagList.json");
        FlagData flags = SaveUtility.JsonToData<FlagData>(flagFilePath);
        _flags = flags.Flags;
        SaveFlag();
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

    public void SetReiStatus(int status)
    {
        PlayerPrefs.SetInt("ReiStatus", status);
        ReiStatus = status;
        if (status == 0) return;
        _flags[$"Symptoms{status}"] = true;
    }

    public void DeleteFlagFile()
    {
        File.Delete(_flagSaveFilePath);
        SaveInitFlags();
    }
}

