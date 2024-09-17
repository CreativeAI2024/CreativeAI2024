using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    private InputSetting _inputSetting;
    private ItemList itemList;
    private string itemName;
    private GameObject itemImageScreen;
    private CShowImage cShowImage;
    void Start()
    {
        //TODO: アイテム画像表示、合成するメソッドを持ったC#のクラスを作り、ShowImageなどでは、そのクラスをインスタンス化する
        _inputSetting = InputSetting.Load();
        itemList = Resources.Load<ItemList>("Items/ItemList");
        itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        itemImageScreen = GameObject.FindWithTag("ItemImageScreen");
        cShowImage = new CShowImage(itemImageScreen);
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (_inputSetting.GetDecideKeyDown())
            {
                Debug.Log("Z Pushed.");
                Debug.Log("itemName: "+itemName);
                cShowImage.Show(itemList.Search(itemName));

            }
            else if (_inputSetting.GetCancelKeyDown() || _inputSetting.GetDecideKeyDown() || _inputSetting.GetMenuKeyDown())
            {
                cShowImage.ChangeEnabled(false);
            }
        }
    }
}