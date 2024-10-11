using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetVariablesImageShow : SetVariables
{
    private Sprite itemImage;
    private CSetImageShow cSetImageShow;
    new void Start()
    {
        GameObjectHolder gameObjectHolder = GameObject.FindWithTag("UIManager").GetComponent<GameObjectHolder>();
        GameObject itemImageScreen = gameObjectHolder.ItemImageScreen;
        base.Start();
        Transform actionWindowButtons = transform.GetChild(1).GetChild(0);
        GameObject imageShowButton = Instantiate(gameObjectHolder.ImageShowButtonPrefab, actionWindowButtons);
        OpenWindow openWindow = imageShowButton.GetComponent<OpenWindow>();
        openWindow.currentWindow = gameObjectHolder.ItemWindow;
        openWindow.nextWindow = itemImageScreen;
        itemImage = ((ImageShowItem)thisItem).Image;
        cSetImageShow = new CSetImageShow(itemImageScreen);
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                cSetImageShow.SetNextWindow(transform.parent.parent.gameObject);
                cSetImageShow.SetImage(itemImage);
            }
        }
    }
}