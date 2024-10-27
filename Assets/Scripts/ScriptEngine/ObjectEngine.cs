using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectEngine : MonoBehaviour
{
    private ObjectData[][] _eventObjects;
    private ObjectData[][] _trapEventObjects;
    [FormerlySerializedAs("playerPlayerTileInfo")] [FormerlySerializedAs("playerTilePosition")] [SerializeField] private TileInfo tileInfo;
    [SerializeField] private string mapName;
    [SerializeField] private ItemInventory inventory; 
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
            ObjectData eventObjectData = _eventObjects[tileInfo.GridPosition.x][tileInfo.GridPosition.y];
            if (eventObjectData is null) return;
            if (eventObjectData) return;
        }
        
        if (tileInfo.GridPosition != pastGridPosition) return;
        
        pastGridPosition = tileInfo.GridPosition;
        ObjectData trapObjectData = _trapEventObjects[tileInfo.GridPosition.x][tileInfo.GridPosition.y];
        if (trapObjectData is null) return;
        
        if (trapObjectData.FlagCondition.Flag.All(x => FlagManager.Instance.HasFlag(x.Key) == x.Value))
        {
            CallEvent(trapObjectData.EventName, trapObjectData.FlagCondition.NextFlag);
        }
    }
    
    private void CallEvent(string eventName, KeyValuePair<string, bool>[] nextFlags)
    {
        switch (eventName)
        {
            case "event1":
                break;
            case "event2":
                break;
            case "event3":
                break;
            case "event4":
                break;
            case "event5":
                break;
            case "event6":
                break;
            case "event7":
                break;
            case "event8":
                break;
            case "event9":
                break;
        }
        
        foreach (KeyValuePair<string, bool> nextFlag in nextFlags)
        {
            if (nextFlag.Value)
            {
                FlagManager.Instance.AddFlag(nextFlag.Key);
            }
            else
            {
                FlagManager.Instance.DeleteFlag(nextFlag.Key);
            }
        }
    }
    
    private void MapMove(string mapName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mapName);
    }
    
    private void Conversation(string fileName)
    {
        ConversationTextManager.Instance.Initialize(fileName);
    }
    
    private void GetItem(int itemIndex)
    {
        
    }
    
    private void CombineItem()
    {
        
    }
    
    private void MiniGame(int gameIndex)
    {
        
    }
}
