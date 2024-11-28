using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : DontDestroySingleton<MenuUIManager>
{
    [SerializeField] private Pause pause;
    void Start()
    {
        ConversationTextManager.Instance.OnConversationStart += pause.PauseAll;
        ConversationTextManager.Instance.OnConversationEnd += pause.UnPauseAll;

    }
}
