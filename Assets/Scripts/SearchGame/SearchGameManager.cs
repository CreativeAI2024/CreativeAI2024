using UnityEngine;

public class SearchGameManager : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private Pause pause;
    [SerializeField] private GameObject main;
    [SerializeField] private SearchGameCursorTip cursorTip;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        ConversationTextManager.Instance.ResetAction();
        ConversationTextManager.Instance.OnConversationStart += Pause;
        ConversationTextManager.Instance.OnConversationEnd += UnPause;
    }

    void Update()
    {
        if (_inputSetting.GetCancelKeyDown())
        {
            Inactivate();
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
    }
}
