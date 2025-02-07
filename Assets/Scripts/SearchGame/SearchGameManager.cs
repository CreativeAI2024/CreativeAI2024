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
    [SerializeField] private GameObject[] interactiveItems;
    [SerializeField] ItemInventory itemInventory;
    [SerializeField] Item[] items;
    [SerializeField] ItemDatabase itemDatabase;
    private bool isInactiveAdded = false;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        ConversationTextManager.Instance.OnConversationStart += Pause;
        ConversationTextManager.Instance.OnConversationEnd += UnPause;
        for (int i = 0; i < interactiveItems.Length; i++)
        {
            if (itemInventory.IsContains(items[i]) || (itemInventory.IsContains(itemDatabase.GetItem("BugsInJar")) && FlagManager.Instance.HasFlag("StartSearchGame1")))
            {
                interactiveItems[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        if (_inputSetting.GetCancelKeyDown())
        {
            Inactivate();
            SoundManager.Instance.PlaySE(7);
        }
        if (!interactiveItems.Any(interactiveItem => interactiveItem.activeSelf) && !isInactiveAdded)
        {
            isInactiveAdded = true;
            ConversationTextManager.Instance.OnConversationEnd += Inactivate;
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

    public void Inactivate()
    {
        ConversationTextManager.Instance.OnConversationStart -= Pause;
        ConversationTextManager.Instance.OnConversationEnd -= UnPause;
        ConversationTextManager.Instance.OnConversationEnd -= Inactivate;
        if (FlagManager.Instance.HasFlag("Broken_A"))
        {
            ChangeFlagAndScene("StartSearchGame1", "itemA_room_broken");
            ChangeFlagAndScene("StartSearchGame2", "itemA_room_broken");
        }
        else
        {
            ChangeFlagAndScene("StartSearchGame1", "itemA_room");
            ChangeFlagAndScene("StartSearchGame2", "itemA_room");
        }
        if(FlagManager.Instance.HasFlag("Broken_B"))
        {
            ChangeFlagAndScene("StartSearchGame3", "itemB_room_broken");
        }
        else
        {
            ChangeFlagAndScene("StartSearchGame3", "itemB_room");
        }
    }

    private void ChangeFlagAndScene(string flag, string scene)
    {
        if (FlagManager.Instance.HasFlag(flag))
        {
            isInactiveAdded = false;
            FlagManager.Instance.DeleteFlag(flag);
            SceneManager.LoadScene(scene);
        }
    }
}
