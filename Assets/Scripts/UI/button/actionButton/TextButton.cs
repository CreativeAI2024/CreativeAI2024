using System;
using TMPro;
using UnityEngine;

public class TextButton : ItemActionButton
{
    //[SerializeField] private TextWindow textWindow;
    [SerializeField] private GameObject conversationWindow;
    public override void OnDecideKeyDown()
    {
        //会話ウィンドウにテキストを渡す処理。渡し方未確定。
        //textWindow.Initialize();
        //textWindow.OnDecideKeyDown();
    }
}