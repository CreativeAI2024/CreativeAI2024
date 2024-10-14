using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageButton : ActionButton
{
    private Sprite itemImage;
    private GameObject itemImageScreen;
    new void Start()
    {
        base.Start();
        itemImageScreen = gameObjectHolder.ItemImageScreen;
        openWindow.NextWindow = itemImageScreen;
        itemImage = thisItem.Image;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Display";
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