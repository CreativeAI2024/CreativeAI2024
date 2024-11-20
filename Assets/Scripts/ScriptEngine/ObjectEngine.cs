using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    private static Queue<string> s_events = new Queue<string>();
    private string _mapName;
    private Vector2Int _pastGridPosition = new Vector2Int(-1, -1);
    private Vector2Int _oldGridPosition = new Vector2Int(-1, -1);
    private InputSetting _inputSetting;
    private void Start()
    {
        _inputSetting = InputSetting.Load();
        _mapName = SceneManager.GetActiveScene().name;
        mapDataController.LoadMapData(_mapName);
        mapEngine.Initialize();
        mapDataController.SetChange(ResetAction);
        ResetAction();
        CallEvent();
    }
    
    private void ResetAction()
    {
        Initialize(_mapName, mapDataController.GetMapSize().x, mapDataController.GetMapSize().y);
    }
    
    // Start is called before the first frame update
    private void Initialize(string mapName, int width, int height)
    {
        ConversationTextManager.Instance.OnConversationEnd += UnPause;
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
            if (!mapDataController.IsGridPositionOutOfRange(player.GetGridPosition() + player.Direction))
            {
                ObjectData aroundObjectData = _eventObjects[player.GetGridPosition().x + player.Direction.x][player.GetGridPosition().y + player.Direction.y];
                if (Call(aroundObjectData, 1, 2))
                {
                    return;
                }
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
    
    private void UnPause()
    {
        pause.UnPauseAll();
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
        string[] eventNames = objectData.EventName.Split(" | ");
        foreach (string eventName in eventNames)
        {
            s_events.Enqueue(eventName);
        }
        CallEvent();
        if (objectData.FlagCondition.NextFlag is not null)
        {
            SetNextFlag(objectData.FlagCondition.NextFlag);
        }
        return true;
    }
    
    private void CallEvent()
    {
        while (s_events.Any())
        {
            var eve = s_events.Dequeue();
            string[] eventArgs = eve.Split(' ');
            string eventName = eventArgs[0];
            switch (eventName)
            {
                case "PlayerMove":
                    PlayerMove(eventArgs[1]);
                    break;
                case "ChangeScene":
                    SceneChange(eventArgs[1]);
                    return;// シーンをまたいだ処理はキューに追加してシーン変更後に実行するため、先に呼ばれないように関数を抜ける
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
                    TileModify(eventArgs[1], Enum.Parse<MapDataController.TileLayer>(eventArgs[2]), position,
                        eventArgs[4].ToCharArray()[0]);
                    break;
                default: throw new NotImplementedException();
            }
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
        SceneManager.LoadScene(sceneName);
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
    }
    
    private void GetItem(string itemName)
    {
        DebugLogger.Log("GetItem", DebugLogger.Colors.Green);
        pause.PauseAll();
        ConversationTextManager.Instance.InitializeFromString($"{itemName}を手に入れた。");
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
