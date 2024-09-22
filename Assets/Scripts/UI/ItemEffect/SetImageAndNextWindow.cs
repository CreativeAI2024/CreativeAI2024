using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetImageAndNextWindow : MonoBehaviour
{
    private GameObject uiManager;
    private Sprite itemImage;
    private GameObject itemImageScreen;
    void Start()
    {
        uiManager = GameObject.FindWithTag("UIManager");
        ItemList itemList = Resources.Load<ItemList>("Items/ItemList");
        itemImageScreen = uiManager.GetComponent<GameObjectHolder>().ItemImageScreen;
        string itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        itemImage = ((ImageShowItem)itemList.Search(itemName)).Image;
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
                //Combineするなら、右辺をConfirmWindowにする
                itemImageScreen.GetComponent<OpenWindow>().nextWindow = transform.parent.parent.gameObject;
                itemImageScreen.GetComponent<Image>().sprite = itemImage;
        }
    }
}