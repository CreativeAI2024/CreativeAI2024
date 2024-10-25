using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageButton : ItemActionButton
{
    [SerializeField] private ItemImageWindow itemImageWindow;
    [SerializeField] private Image screenImage;
    
    public override void OnDecideKeyDown()
    {
        //ItemImageWindowを開き、screenImageを表示する処理
        EventSystem.current.SetSelectedGameObject(gameObject);
        itemImageWindow.OnDecide(window);
        screenImage.sprite = item.Sprite;
    }
}