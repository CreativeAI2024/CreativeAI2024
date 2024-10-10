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
        ItemInventory itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        itemImageScreen = uiManager.GetComponent<GameObjectHolder>().ItemImageScreen;
        itemImage = ((ImageShowItem)itemInventory.GetItem(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)).Image; //押したボタンのテキストからアイテムを取得 かなた質問：ボタンオブジェクトにアイテムボタンを保管するスクリプト作った方がいい？
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