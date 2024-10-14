using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageButton : ActionButton
{
    private GameObject itemImageScreen;
    private Image imageComponent;

    new void Start()
    {
        base.Start();
        itemImageScreen = gameObjectHolder.ItemImageScreen;
        imageComponent = itemImageScreen.GetComponent<Image>();
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                openWindow.NextWindow = itemImageScreen;
                imageComponent.sprite = thisItem.Image;
            }
        }
    }
}