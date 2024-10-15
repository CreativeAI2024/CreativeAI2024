using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextButton : ItemActionButton
{
    private GameObject conversationWindow;
    //会話ウィンドウはInstantiate()か、SetActive(true)で起動するか未確定。TextButtonは暫定的。
    void Start()
    {
        BaseStart();
        conversationWindow = gameObjectHolder.ConversationWindow;
        openWindow.NextWindow = conversationWindow;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Display";
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                //会話ウィンドウにテキストを渡す処理。渡し方未確定。
            }
        }
    }
}