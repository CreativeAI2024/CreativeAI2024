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
            Vector2Int[] directions = new Vector2Int[]
            {
                new Vector2Int(1, 0),
                new Vector2Int(0, -1),
                new Vector2Int(-1, 0),
                new Vector2Int(1, 0)
            };
            for (int i = 0; i < 4; i++)
            {
                ObjectData aroundObjectData = _eventObjects[tileInfo.GridPosition.x+directions[i].x][tileInfo.GridPosition.y+directions[i].y];
                if (aroundObjectData is null) continue;
                if (aroundObjectData.TriggerType != 1) continue;
                if ( /*プレイヤーの向きがイベントオブジェクトのほうを向いていなかったら*/true) continue;
                
                
            }
            ObjectData centerObjectData = _eventObjects[tileInfo.GridPosition.x][tileInfo.GridPosition.y];
            if (centerObjectData is not null && centerObjectData.TriggerType == 2 && FlagCheck(centerObjectData))
            {
                CallEvent(centerObjectData.EventName, centerObjectData.FlagCondition.NextFlag);
            }
        }
        
        if (tileInfo.GridPosition != pastGridPosition) return;
        
        pastGridPosition = tileInfo.GridPosition;
        ObjectData trapObjectData = _trapEventObjects[tileInfo.GridPosition.x][tileInfo.GridPosition.y];
        if (trapObjectData is null) return;
        if (!FlagCheck(trapObjectData)) return;
        
        CallEvent(trapObjectData.EventName, trapObjectData.FlagCondition.NextFlag);
    }
    
    private bool FlagCheck(ObjectData objectData) => objectData.FlagCondition.Flag.All(x => FlagManager.Instance.HasFlag(x.Key) == x.Value);
    
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
