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
    void Start()
    {
        _inputSetting = InputSetting.Load(); 
        ConversationTextManager.Instance.OnConversationStart += Pause;
        ConversationTextManager.Instance.OnConversationEnd += UnPause;
    }

    void Update()
    {
        if (_inputSetting.GetCancelKeyDown())
        {
            Inactivate();
        }
        if (!interactiveItems.Any(interactiveItem => interactiveItem.activeSelf))
        {
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
