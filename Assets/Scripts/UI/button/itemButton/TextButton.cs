using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextButton : ItemButton
{
    private List<string> itemText;
    private GameObject conversationWindow;
    //会話ウィンドウはInstantiate()か、SetActive(true)で起動するか未確定。TextButtonは暫定的。
    new void Start()
    {
        base.Start();
        itemText = thisItem.Text;
        conversationWindow = gameObjectHolder.ConversationWindow;
        openWindow.nextWindow = conversationWindow;
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