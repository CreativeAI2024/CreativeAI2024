using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    private InputSetting _inputSetting;
    private ItemList itemList;
    private string itemName;
    private GameObject ImageOfImageShowItem;
    private bool IsImageEnabled;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        itemList = Resources.Load<ItemList>("Items/ItemList");
        itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        ImageOfImageShowItem = GameObject.FindWithTag("ImageOfImageShowItem");
        IsImageEnabled = false;
    }
    //TODO: 画像表示中は画像を閉じる操作以外は受け付けないようにしてもらうようにシステム班に伝える
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (!IsImageEnabled && _inputSetting.GetDecideKeyDown())
            {
                switch (itemList.Search(itemName))
                {
                    case ImageShowItem imageShowItem:
                        ShowImage(imageShowItem);
                        break;
                }

            }
            else if (IsImageEnabled && (_inputSetting.GetCancelKeyDown() || _inputSetting.GetDecideKeyDown() || _inputSetting.GetMenuKeyDown()))
            {
                ChangeEnabled(false);
            }
        }
    }
    //TODO: もっと便利に、いろんな場面で使える、特定のキー操作をロックするシステムの構築が必要
    //TODO: アイテム画像は1600*900で出してもらうことを視覚班に伝える
    private void ShowImage(ImageShowItem imageShowItem)
    {
        Sprite image = imageShowItem.Image;
        ImageOfImageShowItem.GetComponent<Image>().sprite = image;
        ChangeEnabled(true);
    }

    private void ChangeEnabled(bool IsEnabled)
    {
        ImageOfImageShowItem.GetComponent<Image>().enabled = IsEnabled;
        IsImageEnabled = IsEnabled;
    }
}