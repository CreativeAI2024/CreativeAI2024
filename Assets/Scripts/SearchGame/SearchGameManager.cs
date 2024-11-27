using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SearchGameManager : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private Pause pause;
    [SerializeField] private GameObject main;
    [SerializeField] private SearchGameCursorTip cursorTip;
    [SerializeField] private Item[] items;
    [SerializeField] private GameObject[] interactiveItems;
    private Dictionary<Item, GameObject> itemDict;
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private ItemDatabase itemDatabase;
    void Start()
    {
        _inputSetting = InputSetting.Load(); 
        itemDict = new Dictionary<Item, GameObject>();
        for (int i = 0; i < items.Length; i++)
        {
            itemDict[items[i]] = interactiveItems[i];
        }
        ConversationTextManager.Instance.OnConversationStart += Pause;
        ConversationTextManager.Instance.OnConversationEnd += UnPause;
    }

    void Update()
    {
        if (_inputSetting.GetCancelKeyDown())
        {
            Inactivate();
        }
        foreach (var item in itemDict)
        {
            if (itemInventory.IsContains(item.Key))
            {
                item.Value.SetActive(false);
            }
        }
        if (itemInventory.IsContains(itemDatabase.GetItem("虫の死骸")) && itemInventory.IsContains(itemDatabase.GetItem("空の瓶")))
        {
            ConversationTextManager.Instance.OnConversationEnd += ItemConbine;
        }
        if (itemInventory.IsContains(itemDatabase.GetItem("虫入り瓶")) && !FlagManager.Instance.HasFlag("Worm"))
        {
            ConversationTextManager.Instance.InitializeFromString($"虫の死骸を空の瓶へ入れ、{itemInventory.GetItem("虫入り瓶").ItemName}を作成した。<br>{itemInventory.GetItem("虫入り瓶").DescriptionText}");
            ConversationTextManager.Instance.OnConversationEnd += AddWormFlag;
        }
        if (itemInventory.IsContains(itemDatabase.GetItem("ナイフ")) && !FlagManager.Instance.HasFlag("Knife"))
        {
            ConversationTextManager.Instance.OnConversationEnd += AddKnifeFlag;
        }
        if (!interactiveItems.Any(interactiveItem => interactiveItem.activeSelf) && (FlagManager.Instance.HasFlag("Worm") || FlagManager.Instance.HasFlag("Knife")))
        {
            Inactivate();
            ConversationTextManager.Instance.InitializeFromString("もう何も見当たらない。");
        }
    }

    private void Pause()
    {
        DebugLogger.Log("Pause");
        pause.PauseAll();
    }
    private void UnPause()
    {
        DebugLogger.Log("UnPause");
        pause.UnPauseAll();
    }

    private void ItemConbine()
    {
        itemInventory.TryCombine(itemDatabase.GetItem("虫の死骸"));
    }

    private void AddWormFlag()
    {
        FlagManager.Instance.AddFlag("Worm");
    }
    private void AddKnifeFlag()
    {
        FlagManager.Instance.AddFlag("Knife");
    }

    public void Inactivate()
    {
        ConversationTextManager.Instance.OnConversationEnd -= UnPause;
        main.SetActive(false);
        cursorTip.Reset();
        ChangeFlagAndScene("StartSearchGame1", "itemA_room");
        ChangeFlagAndScene("StartSearchGame2", "itemA_room");
        ChangeFlagAndScene("StartSearchGame3", "itemB_room");
    }

    private void ChangeFlagAndScene(string flag, string scene)
    {
        if (FlagManager.Instance.HasFlag(flag))
        {
            FlagManager.Instance.DeleteFlag(flag);
            SceneManager.LoadScene(scene);
        }
    }
}
