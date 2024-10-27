using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlagManager : DontDestroySingleton<FlagManager>
{
    [SerializeField] private string[] flagList; 
    private Dictionary<string, bool> _flags;
    private readonly string _flagFilePath = string.Join('/', Application.persistentDataPath, "FlagData.dat");
    public override void Awake()
    {
        base.Awake();
        if (!File.Exists(_flagFilePath))
        {
            FlagData flags = SaveUtility.SaveFileToData<FlagData>(_flagFilePath);
            _flags = flags.Flags;
        }
        else
        {
            Debug.Log("フラグデータがありませんでした");
            foreach (string flag in flagList)
            {
                _flags.Add(flag, false);
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
        SaveUtility.DataToSaveFile(saveFlagData, _flagFilePath);
    }
    
    public bool HasFlag(string flagName) => _flags[flagName];
}

