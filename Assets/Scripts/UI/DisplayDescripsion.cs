using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayDescripsion : MonoBehaviour
{
    private GameObject focusedButton;
    void Start()
    {
        focusedButton = EventSystem.current.currentSelectedGameObject; //ボタンが切り替わったら表示テキストを変更する
    }
    void Update()
    {
        
    }
    private void SetText()
    {
        
    }
}
