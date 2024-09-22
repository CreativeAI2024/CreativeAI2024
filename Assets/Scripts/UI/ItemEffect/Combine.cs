using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Combine : MonoBehaviour
{
    private InputSetting _inputSetting;
    private ItemList itemList;
    private string itemName;
    private GameObject itemImageScreen;
    private CShowImage cShowImage;
    private CCombine cCombine;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        itemList = Resources.Load<ItemList>("Items/ItemList");
        itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        itemImageScreen = GameObject.FindWithTag("ItemImageScreen");
        cShowImage = new CShowImage(itemImageScreen);
        cCombine = new CCombine(itemList, itemName);
    }
    void Update()
    {
      //TODO: 決定で合成できるか判断、できるなら確認ウィンドウを出し、yesで合成。合成できないならそれっぽい効果音出す
      //TODO: ComfirmWindowのYesButtonにアタッチする
      //TODO: ComfirmButtonに自分とペアアイテムを渡す必要がある
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (_inputSetting.GetDecideKeyDown())
            {
                // Debug.Log("Z Pushed.");
                // Debug.Log("itemName: "+itemName);
                cCombine.Combine();

            }
        }
    }
}