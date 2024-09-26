using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetVarImageShow : MonoBehaviour
{
    private InputSetting _inputSetting;
    private GameObject uiManager;
    private Sprite itemImage;
    private GameObject itemImageScreen;
    private CSetImageShow cSetImageShow;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        uiManager = GameObject.FindWithTag("UIManager");
        ItemList itemList = Resources.Load<ItemList>("Items/ItemList");
        itemImageScreen = uiManager.GetComponent<GameObjectHolder>().ItemImageScreen;
        string itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        itemImage = ((ImageShowItem)itemList.Search(itemName)).Image;
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