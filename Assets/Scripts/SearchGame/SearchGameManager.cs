using UnityEngine;

public class SearchGameManager : MonoBehaviour
{
    private InputSetting _inputSetting;
    private bool lastIsConversationWindowActive = false;
    [SerializeField] private Pause pause;
    [SerializeField] private GameObject main;
    [SerializeField] private SearchGameCursorTip cursorTip;
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }

    void Update()
    {
        if (lastIsConversationWindowActive)
        {
            if (!ConversationTextManager.Instance.GetInitializeFlag())
            {
                lastIsConversationWindowActive = false;
                pause.UnPauseAll();
            }
        }
        else
        {
            if (ConversationTextManager.Instance.GetInitializeFlag())
            {
                lastIsConversationWindowActive = true;
                pause.PauseAll();
            }
            if (_inputSetting.GetCancelKeyDown())
            {
                Inactivate();
            }
        }
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
