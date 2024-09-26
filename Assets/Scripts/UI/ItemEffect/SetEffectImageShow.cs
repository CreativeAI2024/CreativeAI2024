using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetEffectImageShow : MonoBehaviour
{
    private InputSetting _inputSetting;
    private GameObject uiManager;
    private Sprite itemImage;
    private GameObject itemImageScreen;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        uiManager = GameObject.FindWithTag("UIManager");
        ItemList itemList = Resources.Load<ItemList>("Items/ItemList");
        itemImageScreen = uiManager.GetComponent<GameObjectHolder>().ItemImageScreen;
        string itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        itemImage = ((ImageShowItem)itemList.Search(itemName)).Image;
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                itemImageScreen.GetComponent<OpenWindow>().nextWindow = transform.parent.parent.gameObject;
                itemImageScreen.GetComponent<Image>().sprite = itemImage;
            }
        }
    }
}