using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectEngine : MonoBehaviour
{
    private ObjectData[][] _eventObjects;
    private ObjectData[][] _trapEventObjects;
    
    [FormerlySerializedAs("tileInfo")] [SerializeField] private PlayerController player;
    [SerializeField] private ItemInventory inventory;
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private Pause pause;
    private Vector2Int _pastGridPosition = new Vector2Int(-1, -1);
    private Vector2Int _oldGridPosition = new Vector2Int(-1, -1);
    private string itemTextJson = "incorrect";
    private bool talkFlag = false;
    private InputSetting _inputSetting;
    private void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    
    // Start is called before the first frame update
    public void Initialize(string mapName, int width, int height)
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
        ConversationTextManager.Instance.OnConversationEnd += pause.UnPauseAll;
    }
    
    private void Update()
    {
        if (_inputSetting.GetDecideInputDown())
        {
            ObjectData aroundObjectData = _eventObjects[player.GetGridPosition().x + player.Direction.x][player.GetGridPosition().y + player.Direction.y];
            if (Call(aroundObjectData, 1, 2))
            {
                DebugLogger.Log("eee");
                return;
            }
            ObjectData centerObjectData = _eventObjects[player.GetGridPosition().x][player.GetGridPosition().y];
            if (Call(centerObjectData, 2))
            {
                return;
            }
        }
        
        if (player.GetGridPosition() == _pastGridPosition) return;// centerObjectData.TriggerType == 0 
        _pastGridPosition = player.GetGridPosition();
        ObjectData trapObjectData = _trapEventObjects[player.GetGridPosition().x][player.GetGridPosition().y];
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
            case "PaperGame":
                DebugLogger.Log("papergame");
                break;
            case "SearchGame":
                DebugLogger.Log("searchgame");
                break;
            case "TimingGame":
                DebugLogger.Log("timinggame");
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
        DebugLogger.Log("Conversation", DebugLogger.Colors.Cyan);
        pause.PauseAll();
        ConversationTextManager.Instance.InitializeFromJson(fileName);
        talkFlag = true;
    }
    
    private void GetItem(string itemName)
    {
        DebugLogger.Log("GetItem", DebugLogger.Colors.Green);
        Conversation(itemTextJson);
        Item item = itemDatabase.GetItem(itemName);
        inventory.Add(item);
        CombineItem(item);
    }
    
    private void CombineItem(Item item)
    {
        if (inventory.IsContains(item))
        {
            inventory.TryCombine(item);
        }
    }
}
