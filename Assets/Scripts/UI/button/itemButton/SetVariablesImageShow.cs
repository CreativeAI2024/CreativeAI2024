using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetVariablesImageShow : SetVariables
{
    private Sprite itemImage;
    private GameObject itemImageScreen;
    private CSetImageShow cSetImageShow;
    new void Start()
    {
        base.Start();
        itemImage = ((ImageShowItem)thisItem).Image;
        itemImageScreen = uiManager.GetComponent<GameObjectHolder>().ItemImageScreen;
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