using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageButton : ItemButton
{
    private Sprite itemImage;
    private GameObject itemImageScreen;
    new void Start()
    {
        base.Start();
        itemImageScreen = gameObjectHolder.ItemImageScreen;
        openWindow.nextWindow = itemImageScreen;
        itemImage = thisItem.Image;
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                itemImageScreen.GetComponent<Image>().sprite = itemImage;
            }
        }
    }
}