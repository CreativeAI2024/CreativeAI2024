using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEngine : MonoBehaviour
{
    private ObjectData[][] _eventObjects;
    private ObjectData[][] _trapEventObjects;
    
    [SerializeField] private PlayerController player;
    [SerializeField] private ItemInventory inventory;
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private Pause pause;
    
    [SerializeField] private MapEngine mapEngine;
    [SerializeField] private MapDataController mapDataController;
    
    [SerializeField] private string mapName;
    private Vector2Int _pastGridPosition = new Vector2Int(-1, -1);
    private Vector2Int _oldGridPosition = new Vector2Int(-1, -1);
    private string itemTextJson = "incorrect";
    private bool talkFlag = false;
    private InputSetting _inputSetting;
    private void Start()
    {
        _inputSetting = InputSetting.Load();
        
        mapDataController.LoadMapData(mapName);
        mapEngine.Initialize();
        int width = mapDataController.GetMapSize().x;
        int height = mapDataController.GetMapSize().y;
        mapDataController.SetChange(ResetAction);
        Initialize(mapName, width, height);
    }
    
    private void ResetAction()
    {
        Initialize(mapName, mapDataController.GetMapSize().x, mapDataController.GetMapSize().y);
    }
    
    // Start is called before the first frame update
    private void Initialize(string mapName, int width, int height)
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
        if (_inputSetting.GetDecideInputDown())
        {
            ObjectData aroundObjectData = _eventObjects[player.GetPlayerGridPosition().x + player.Direction.x][player.GetPlayerGridPosition().y + player.Direction.y];
            if (Call(aroundObjectData, 1, 2))
            {
                return;
            }
            ObjectData centerObjectData = _eventObjects[player.GetPlayerGridPosition().x][player.GetPlayerGridPosition().y];
            if (Call(centerObjectData, 2))
            {
                return;
            }
        }
        
        if (talkFlag && !ConversationTextManager.Instance.GetInitializeFlag())
        {
            pause.UnPauseAll();
            talkFlag = false;
        }
        
        if (player.GetPlayerGridPosition() == _pastGridPosition) return;// centerObjectData.TriggerType == 0 
        _pastGridPosition = player.GetPlayerGridPosition();
        ObjectData trapObjectData = _trapEventObjects[player.GetPlayerGridPosition().x][player.GetPlayerGridPosition().y];
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
        foreach (string eventString in objectData.EventName.Split(" | "))
        {
            CallEvent(eventString);
        }
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
            case "PlayerMove":
                PlayerMove(eventArgs[1]);
                break;
            case "SceneChange":
                SceneChange(eventArgs[1]);
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
            case "TileModify":
                string[] positionStr = eventArgs[3].Split(',');
                Vector2Int position = new Vector2Int(int.Parse(positionStr[0]), int.Parse(positionStr[1]));
                TileModify(eventArgs[1], Enum.Parse<MapDataController.TileLayer>(eventArgs[2]), position, eventArgs[4].ToCharArray()[0]);
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
    
    private void SceneChange(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    
    private void PlayerMove(string movedPos)
    {
        string[] position = movedPos.Split(',');
        int movedX = int.Parse(position[0]);
        int movedY = int.Parse(position[1]);
        player.transform.position = new Vector3(movedX, movedY, 0);
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
    
    private void TileModify(string mapName, MapDataController.TileLayer layer, Vector2Int position, char tipSign)
    {
        mapDataController.ChangeMapTile(mapName, layer, position, tipSign);
        mapDataController.ApplyMapChange();
    }
}
