using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
// やること
// ItemWindowを開いたときに、生成されたitemButtonにitemNameが読み込まれていない。
// ウィンドウを再表示した時にsetFirstFocusができていない OnEnableは親がenableになっても関係ない？
// カーソルは１つだけでいいのでは？
// カーソルの移動先が無い時にerrorが起きる
// デバッグ
// 説明ウィンドウの機能作成
// ImageShowItemのimageを表示する機能
// ItemButtonに個数表示機能
// メニューUIの構造の見直し（自分以外でも使えるように）
//     スクリプトをどのオブジェクトにアタッチすれば良いか
//     プレハブを実体化すればすぐメニューUIが使えるようにする
//     SerializeFieldは本当にこのスクリプトで必要か
public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemList itemList;
    [SerializeField] private GameObject itemButtonPrefab;
    // private List<string> 

    void Start()
    {
        foreach (BaseItem item in itemList.Items)
        {
            MakeItemButton(item);
        }
        AddSetFirstFocusComponent();
    }

    // void Start()
    // {
    //     foreach (Transform child in transform)
    //     {
    //         SetButtonName(child.gameObject, );
    //     }
    // }

    public GameObject Search(string searchedButtonName) //アイテム合成の時にリストを探索する時に呼び出す
    {
        foreach (Transform child in transform)
        {
            GameObject button = child.gameObject;
            if (GetButtonName(button).Equals(searchedButtonName))
            {
                // Debug.Log(searchedButtonName + "is searched.");
                return button;
            }
        }
        return null;
    }

    private void CheckDuplication() //他のメソッド内で呼び出してチェックする　のは重そう
    {
        HashSet<string> checkerSet = new();
        foreach (BaseItem item in itemList.Items)
        {
            if (!checkerSet.Add(item.ItemName))
            {
                throw new InvalidOperationException("ItemWindow内で" + item + "ボタンが重複しています。");
            }
        }
    }

    public void Add(BaseItem item) //アイテムをゲットした時に
    {
        itemList.Add(item);
        GameObject addedButton = this.Search(item.ItemName);
        if (addedButton == null)
        {
            MakeItemButton(item);
            // Debug.Log("New itemButton "+item.ItemName+" is generated.");
        }
        else
        {
            itemList.Search(item.ItemName).IncrementCount();
            // Debug.Log("The count of "+item.ItemName+"is incremented.");
        }
        CheckDuplication();
    }

    public void Remove(BaseItem item) //消耗品のアイテム等を使った時に
    {
        itemList.Remove(item);
        GameObject removedButton = this.Search(item.ItemName);
        if (removedButton == null)
        {
            // Debug.Log("存在しない"+item.ItemName+"ボタンを削除しようとしています。");
        }
        else if (itemList.Search(item.ItemName).Count > 1)
        {
            itemList.Search(item.ItemName).DecrementCount();
            // Debug.Log("The count of "+item.ItemName+"is decremented.");
        }
        else
        {
            Destroy(removedButton);
            itemList.Remove(item);
            AddSetFirstFocusComponent();
            // Debug.Log("The itemButton "+item.ItemName+" destroyed.");
        }
        CheckDuplication();
    }

    private void MakeItemButton(BaseItem item)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, transform);
        Debug.Log("itemButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text: "+itemButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        Debug.Log("item.ItemName: "+item.ItemName);
        //次のフレームで実行する
        SetButtonName(itemButton, item.ItemName);
        Debug.Log("SetButtonName() finish.");
        Debug.Log("itemButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text: "+itemButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
    }

    private string GetButtonName(GameObject button)
    {
        return button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }

    private void SetButtonName(GameObject button, string buttonName)
    {
        Debug.Log("button: "+button);
        Debug.Log("button.transform.GetChild(0): "+button.transform.GetChild(0));
        Debug.Log("button.transform.GetChild(0).GetComponent<TextMeshProUGUI>(): "+button.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        Debug.Log("button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text: "+ button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonName;
    }

    private void AddSetFirstFocusComponent()
    {
        transform.GetChild(0).AddComponent<SetFirstFocus>();
    }
}