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
    
    [SerializeField] private TileInfo tileInfo;
    [SerializeField] private string mapName;
    [SerializeField] private ItemInventory inventory;
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private Pause pause;
    private Vector2Int _pastGridPosition = new Vector2Int(-1, -1);
        
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
            if (filePath.EndsWith(".meta")) continue;
            
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
        if (_inputSetting.GetDecideKeyDown())
        {
            DebugLogger.Log(tileInfo.GridPosition);
            ObjectData aroundObjectData = _eventObjects[tileInfo.GridPosition.x + tileInfo.Direction.x][tileInfo.GridPosition.y + tileInfo.Direction.y];
            if (Call(aroundObjectData, 1, 2))
            {
                return;
            }
            ObjectData centerObjectData = _eventObjects[tileInfo.GridPosition.x][tileInfo.GridPosition.y];
            if (Call(centerObjectData, 2))
            {
                return;
            }
        }
        
        if (_pastGridPosition == new Vector2Int(-1, -1) || tileInfo.GridPosition == _pastGridPosition) return;// centerObjectData.TriggerType == 0 
        
        _pastGridPosition = tileInfo.GridPosition;
        ObjectData trapObjectData = _trapEventObjects[tileInfo.GridPosition.x][tileInfo.GridPosition.y];
        Call(trapObjectData, 0);
    }
    
    private bool Call(ObjectData objectData, params int[] triggerType)
    {
        if (objectData is null) return false;
        if (!triggerType.Contains(objectData.TriggerType)) return false;
        if (objectData.FlagCondition.Flag is not null && 
            objectData.FlagCondition.Flag.Any(x => FlagManager.Instance.HasFlag(x.Key) != x.Value))
        {
            return false;
        }
        
        CallEvent(objectData.EventName);
        if (objectData.FlagCondition.NextFlag is not null)
        {
            SetNextFlag(objectData.FlagCondition.NextFlag);
        }
        return true;
    }
    
    private void CallEvent(string eventString)
    {
        string[] eventArgs = eventString.Split(' ');
        string eventName = eventArgs[0];
        switch (eventName)
        {
            case "MapMove":
                MapMove(eventArgs[1]);
                break;
            case "Conversation":
                Conversation(eventArgs[1]);
                break;
            case "GetItem":
                GetItem(eventArgs[1]);
                break;
            case "CombineItem":
                CombineItem();
                break;
            case "Minigame":
                string eventArg = eventArgs[1];
                CallMiniGame(int.Parse(eventArgs[1]));
                break;
            default: throw new NotImplementedException();
        }
    }
    
    private void SetNextFlag(KeyValuePair<string, bool>[] nextFlags)
    {
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
        pause.PauseAll();
        // ConversationTextManager.Instance.Initialize(fileName);
    }
    
    private void GetItem(string itemName)
    {
        Item item = itemDatabase.GetItem(itemName);
        inventory.Add(item);
    }
    
    private void CombineItem()
    {
        
    }
    
    private void CallMiniGame(int gameIndex)
    {
        switch (gameIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
