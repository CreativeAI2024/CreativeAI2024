using System;
using TMPro;
using UnityEngine;

public class ImageTextButton : ItemActionButton
{
    //[SerializeField] private ImageTextWindow imageTextWindow;
    [SerializeField] private ConversationTextManager conversationManager;
    public override void OnDecideKeyDown()
    {
        //会話ウィンドウに画像を渡す処理。渡し方未確定。
        //imageTextWindow.Initialize();
        //imageTextWindow.OnDecideKeyDown();
    }
}