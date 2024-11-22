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
        foreach (var item in itemDict) {
            if (itemInventory.IsContains(item.Key))
            {
                item.Value.SetActive(false);
            }
        }
        itemInventory.TryCombine(itemInventory.GetItem("Worm"));
        if (itemInventory.IsContains(itemInventory.GetItem("Worm")))
        {
            ConversationTextManager.Instance.InitializeFromString($"{itemInventory.GetItem("Worm").ItemName}を手に入れた<br>{itemInventory.GetItem("Worm").DescriptionText}");
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

    public void Activate()
    {
        main.SetActive(true);
    }
    public void Inactivate()
    {
        main.SetActive(false);
        cursorTip.Reset();
        switch (gameObject.name)
        {
            case "SearchGame 1":
                SceneManager.LoadScene("itemA_room");
                break;
            case "SearchGame 2":
                SceneManager.LoadScene("itemA_room");
                break;
            case "SearchGame 3":
                SceneManager.LoadScene("itemB_room");
                break;
        }
    }
}
