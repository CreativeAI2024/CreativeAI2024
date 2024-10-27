using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEngine : MonoBehaviour
{
    private ObjectData[][] _eventObjects;
    private ObjectData[][] _trapEventObjects;
    [SerializeField] private TilePosition playerTilePosition;
    [SerializeField] private string mapName;
    private Vector2Int pastGridPosition;
        
    private InputSetting _inputSetting;
    private void Start()
    {
        _inputSetting = InputSetting.Load("Player Input Setting");
    }
    
    // Start is called before the first frame update
    public void Initialize(int width, int height)
    {
        _eventObjects = new ObjectData[width][];
        _trapEventObjects = new ObjectData[width][];
        for (int i = 0; i < width; i++)
        {
            _eventObjects[i] = new ObjectData[height];
            _trapEventObjects[i] = new ObjectData[height];
        }
        string assetsPath = string.Join('/', Application.streamingAssetsPath, "ObjectData");
        foreach (string filePath in Directory.GetFiles(assetsPath))
        {
            ObjectData objectData = SaveUtility.JsonToData<ObjectData>(filePath);
            foreach (Location location in objectData.Location)
            {
                if (!location.MapName.Equals(mapName)) continue;
                
                // 自動発動イベント
                if (objectData.TriggerType == 0)
                {
                    _trapEventObjects[location.Position.x][location.Position.y] = objectData;
                }
                else
                {
                    _eventObjects[location.Position.x][location.Position.y] = objectData;
                }
            }
        }
    }
    
    private void Update()
    {
        if (_inputSetting.GetDecideKey())
        {
            
        }
        
        if (playerTilePosition.GridPosition != pastGridPosition) return;
        
        pastGridPosition = playerTilePosition.GridPosition;
        ObjectData trapObjectData = _trapEventObjects[playerTilePosition.GridPosition.x][playerTilePosition.GridPosition.y];
        if (trapObjectData is null) return;
        
        if (trapObjectData.FlagCondition.Flag.All(x => x.Key == x.value));
    }
}
