using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectEngine : MonoBehaviour
{
    private List<ObjectData>[][] _eventObjects;
    private List<ObjectData>[][] _trapEventObjects;
    
    [SerializeField] private PlayerController player;
    [SerializeField] private ItemInventory inventory;
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private Pause pause;
    
    [SerializeField] private MapEngine mapEngine;
    [SerializeField] private MapDataController mapDataController;
    private static Vector2Int changedPos = new Vector2Int(4, 2);
    private string _mapName;
    private Vector2Int _pastGridPosition = new Vector2Int(-1, -1);
    private bool conversationFlag = false;
    private bool changeSceneFlag = false;
    private bool runFlag = false;
    private InputSetting _inputSetting;
    private void Start()
    {
        _inputSetting = InputSetting.Load();
        _mapName = SceneManager.GetActiveScene().name;
        mapDataController.LoadMapData(_mapName);
        
        ConversationTextManager.Instance.ResetAction();
        ConversationTextManager.Instance.OnConversationStart += Pause;
        ConversationTextManager.Instance.OnConversationEnd += UnPause;
        ConversationTextManager.Instance.OnConversationEnd += () => conversationFlag = false;
        mapDataController.SetChange(ResetAction);
        ResetAction();
        PlayerMove(changedPos);
    }
    
    private void ResetAction()
    {
        mapEngine.Initialize();
        Initialize(_mapName, mapDataController.GetMapSize().x, mapDataController.GetMapSize().y);
    }
    
    // Start is called before the first frame update
    private void Initialize(string mapName, int width, int height)
    {
        _eventObjects = new List<ObjectData>[width][];
        _trapEventObjects = new List<ObjectData>[width][];
        for (int i = 0; i < width; i++)
        {
            _eventObjects[i] = new List<ObjectData>[height];
            _trapEventObjects[i] = new List<ObjectData>[height];
            for (int j = 0; j < height; j++)
            {
                _eventObjects[i][j] = new List<ObjectData>(4);
                _trapEventObjects[i][j] = new List<ObjectData>(4);
            }
        }
        IFileAssetLoader loader = SaveUtility.FileAssetLoaderFactory();
        string path = loader.GetPath("ObjectData");
        foreach (string objectFilePath in loader.GetPathDirectory(path))
        {
            if (objectFilePath.EndsWith(".meta")) continue;
            
            ObjectData objectData = SaveUtility.JsonToData<ObjectData>(objectFilePath);
            foreach (Location location in objectData.Location)
            {
                if (!location.MapName.Equals(mapName)) continue;
                
                // 自動発動イベント
                if (objectData.TriggerType == 0 || objectData.TriggerType == 4)
                {
                    _trapEventObjects[location.Position.x][location.Position.y].Add(objectData);
                }
                else
                {
                    _eventObjects[location.Position.x][location.Position.y].Add(objectData);
                }
            }
        }
    }
    
    /*
    [Conditional("UNITY_EDITOR")]
    private void OnDrawGizmos()
    {
        for (int i = 0; i < mapDataController.GetMapSize().y; i++)
        {
            for (int j = 0; j < mapDataController.GetMapSize().x; j++)
            {
                if (_trapEventObjects[j][i].Any())
                {
                    Gizmos.color = Color.yellow;
                    DrawRect(new Rect(j-0.5f, i-0.5f, 1, 1));
                }
                else if (_eventObjects[j][i].Any())
                {
                    Gizmos.color = Color.green;
                    DrawRect(new Rect(j-0.5f, i-0.5f, 1, 1));
                }
            }
        }
    }
    
    [Conditional("UNITY_EDITOR")]
    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
    */
    private async void Update()
    {
        if (conversationFlag || changeSceneFlag) return;
        if (_inputSetting.GetDecideInputDown())
        {
            Vector2Int frontPosition = new Vector2Int(player.GetGridPosition().x + player.Direction.x, player.GetGridPosition().y + player.Direction.y);
            if (!mapDataController.IsGridPositionOutOfRange(frontPosition))
            {
                List<ObjectData> aroundObjectDatas = _eventObjects[frontPosition.x][frontPosition.y];
                runFlag = false;
                foreach (ObjectData aroundObjectData in aroundObjectDatas)
                {
                    await Call(aroundObjectData, 1, 2);
                }
                
                if (runFlag) return;
            }
            
            List<ObjectData> centerObjectDatas = _eventObjects[player.GetGridPosition().x][player.GetGridPosition().y];
            foreach (ObjectData centerObjectData in centerObjectDatas)
            {
                await Call(centerObjectData, 2, 3);
            }
        }
        
        List<ObjectData> trapObjectDatas = _trapEventObjects[player.GetGridPosition().x][player.GetGridPosition().y];
        if (player.GetGridPosition() == _pastGridPosition && !trapObjectDatas.Any(trapObjectData => trapObjectData.TriggerType == 4)) return;// centerObjectData.TriggerType == 0 
        _pastGridPosition = player.GetGridPosition();
        foreach (ObjectData trapObjectData in trapObjectDatas)
        {
            await Call(trapObjectData, 0, 4);
        }
    }
    
    private void Pause()
    {
        DebugLogger.Log("Pause : "+UnityEngine.SceneManagement.SceneManager.GetActiveScene().name+" : "+(pause == null), DebugLogger.Colors.Magenta);
        pause.PauseAll();
    }
    
    private void UnPause()
    {
        DebugLogger.Log("UnPause : "+UnityEngine.SceneManagement.SceneManager.GetActiveScene().name+" : "+(pause == null), DebugLogger.Colors.Magenta);
        pause.UnPauseAll();
    }
    
    private async UniTask Call(ObjectData objectData, params int[] triggerType)
    {
        if (objectData is null) return;
        if (!triggerType.Contains(objectData.TriggerType)) return;
        string[] eventNames = objectData.EventName.Split(" | ");
        foreach (string eventName in eventNames)
        {
            foreach (var x in objectData.FlagCondition.Flag)
            {
                DebugLogger.Log(x.Key+" : expected: "+x.Value+" : actual:"+ FlagManager.Instance.HasFlag(x.Key), DebugLogger.Colors.Blue);
            }
            if (objectData.FlagCondition.Flag is not null && objectData.FlagCondition.Flag.Any(x => FlagManager.Instance.HasFlag(x.Key) != x.Value))
            {
                return; // continue;
            }
            runFlag = true;
            await CallEvent(eventName);
        }
        if (objectData.FlagCondition.NextFlag is not null)
        {
            SetNextFlag(objectData.FlagCondition.NextFlag);
        }
        if (changeSceneFlag) return;
        List<ObjectData> trapObjectDatas = _trapEventObjects[player.GetGridPosition().x][player.GetGridPosition().y];
        DebugLogger.Log("end");
        foreach (ObjectData trapObjectData in trapObjectDatas)
        {
            await Call(trapObjectData, 0, 4);// フラグで制御されてるとはいえ再帰的になっているので要注意
        }
    }
    
    private async UniTask CallEvent(string eventName)
    {
        string[] eventArgs = eventName.Split(' ');
        string eventKey = eventArgs[0];
        switch (eventKey)
        {
            case "PlayerMove":
                DebugLogger.Log("PlayerMove", DebugLogger.Colors.Green);
                string[] pos = eventArgs[1].Split(',');
                Vector2Int moved = new Vector2Int(int.Parse(pos[0]), int.Parse(pos[1]));
                PlayerMove(moved);
                break;
            case "ChangeScene":
                SoundManager.Instance.PlaySE(6, 5f); //ドアくぐる
                DebugLogger.Log("ChangeScene", DebugLogger.Colors.Green);
                string[] args = eventArgs[1].Split(',');
                changedPos = new Vector2Int(int.Parse(args[1]), int.Parse(args[2]));
                await SceneChange(args[0]);
                break;
            case "Conversation":
                DebugLogger.Log("Conversation", DebugLogger.Colors.Green);
                conversationFlag = true;
                Conversation(eventArgs[1]);
                await UniTask.WaitUntil(() => !conversationFlag);
                break;
            case "GetItem":
                DebugLogger.Log("GetItem", DebugLogger.Colors.Green);
                GetItem(eventArgs[1]);
                break;
            case "PaperGame":
                DebugLogger.Log("PaperGame", DebugLogger.Colors.Green);
                changedPos = new Vector2Int(player.GetGridPosition().x, player.GetGridPosition().y);
                await SceneChange("PaperGame");
                break;
            case "SearchGame":
                DebugLogger.Log("SearchGame", DebugLogger.Colors.Green);
                changedPos = new Vector2Int(player.GetGridPosition().x, player.GetGridPosition().y);
                await SceneChange("SearchGame");
                break;
            case "TimingGame":
                DebugLogger.Log("TimingGame", DebugLogger.Colors.Green);
                ConversationTextManager.Instance.OnConversationStart -= Pause;
                changedPos = new Vector2Int(player.GetGridPosition().x, player.GetGridPosition().y);
                await SceneChange("TimingGame");
                break;
            case "TileModify":
                DebugLogger.Log("TileModify", DebugLogger.Colors.Green);
                string[] positionStr = eventArgs[3].Split(',');
                Vector2Int position = new Vector2Int(int.Parse(positionStr[0]), int.Parse(positionStr[1]));
                TileModify(eventArgs[1], Enum.Parse<MapDataController.TileLayer>(eventArgs[2]), position,
                    eventArgs[4].ToCharArray()[0]);
                break;
            case "GoToEndScene":
                DebugLogger.Log("GoToEndScene", DebugLogger.Colors.Green);
                ConversationTextManager.Instance.OnConversationStart -= Pause;
                pause.PauseReset();
                await SceneChange("Ending");
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
    
    private async UniTask SceneChange(string sceneName)
    {
        changeSceneFlag = true;
        await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
        PlayerPrefs.SetString("SceneName", sceneName);
    }
    
    private void PlayerMove(Vector2Int moved)
    {
        player.transform.position = new Vector3(moved.x, moved.y, 0);
    }
    
    private void Conversation(string fileName)
    {
        ConversationTextManager.Instance.InitializeFromJson(fileName);
    }
    
    private void GetItem(string itemName)
    {
        Item item = itemDatabase.GetItem(itemName);
        if (inventory.IsContains(item)) return;
        SoundManager.Instance.PlaySE(9, 5f); //アイテム拾う
        inventory.Add(item);
        CombineItem(item);
        ConversationTextManager.Instance.InitializeFromString($"{item.ItemName}を手に入れた。");
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