using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//TODO: confirmWindowでyes推したのに合成できていない
//TODO: アイテムの機能作る
//TODO:     テキスト表示機能
//TODO: スクロール機能
//TODO: 改行に合わせて説明ウィンドウの縦幅を増やす機能
//TODO: 確認ウィンドウに合成アイテム名を表示
//TODO: アイテム合成の時に、アイテムの個数のことを考えずに問答無用でDestroyしていた
//TODO: ItemButtonに個数表示機能
//TODO: スクリプト実行順見直す
//TODO: デバッグ
//TODO: メニューUIの構造の見直し（自分以外でも使えるように）
//TODO:     プレハブを実体化すればすぐメニューUIが使えるようにする
//カーソル移動ロック機能作る→今のところ不要になった
//アイテム画像は1600*900で出してもらうことを視覚班に伝える→ゲーム全体の解像度によるので保留
//TODO: アイテム周りのクラス図を作る
//ImageShowItem Objectは画像と、そのgetterを持っている
//ItemWindowはButtons内のBaseItemにfor文で全てにアクセスできる
//引数の型がBaseItemでも、実際に引数として渡される変数がそれを継承したImageShowItemクラスなら、ImageShowItemクラスとして認識される

public class ItemButtons : MonoBehaviour
{
    [SerializeField] private ItemList itemList;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private GameObject itemImageScreen;
    private GameObject itemWindow;
    [SerializeField] private GameObject confirmWindow;

    void OnEnable()
    {
        itemWindow = transform.parent.gameObject;
        LoadItemList();
    }

    private void LoadItemList()
    {
        foreach (Transform child in transform)
        {
            if (!itemList.Search(child.GetChild(0).GetComponent<TextMeshProUGUI>().text))
            {
                Destroy(child.gameObject);
            }
        }
        foreach (BaseItem item in itemList.Items)
        {
            if (!Search(item.ItemName))
            {
                MakeItemButton(item.ItemName);
            }
        }
    }

    //アイテム合成の時にリストを探索する時に呼び出す
    //上の場合でも、ItemList内を探せばいいのでは？
    private GameObject Search(string searchedButtonName)
    {
        foreach (Transform child in transform)
        {
            GameObject button = child.gameObject;
            if (GetButtonName(button).Equals(searchedButtonName))
            {
                return button;
            }
        }
        return null;
    }
    private void MakeItemButton(string itemName)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, transform);
        SetButtonName(itemButton, itemName);
        switch (itemList.Search(itemName))
        {
            case ImageShowItem item:
                itemButton.AddComponent<OpenWindow>();
                itemButton.GetComponent<OpenWindow>().currentWindow = itemWindow;
                itemButton.GetComponent<OpenWindow>().nextWindow = itemImageScreen;
                if (item.IsCombinable/*ペアアイテム持っているかどうかを追加*/)
                {
                    itemImageScreen.GetComponent<OpenWindow>().nextWindow = confirmWindow;
                    itemButton.AddComponent<SetVarImageShowCombineMaterial>();
                }
                else
                {
                    itemImageScreen.GetComponent<OpenWindow>().nextWindow = itemWindow;
                    itemButton.AddComponent<SetVarImageShow>();
                }
                break;
            case BaseItem item:
                if (item.IsCombinable/*ペアアイテム持っているかどうかを追加*/)
                {
                    itemButton.AddComponent<OpenWindow>();
                    itemButton.GetComponent<OpenWindow>().currentWindow = itemWindow;
                    itemButton.GetComponent<OpenWindow>().nextWindow = confirmWindow;
                    itemButton.GetComponent<OpenWindow>().enabled = true;
                    itemButton.AddComponent<SetVarCombineMaterial>();
                }
                break;
        }

    }


    private string GetButtonName(GameObject button)
    {
        return button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }

    private void SetButtonName(GameObject button, string buttonName)
    {
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonName;
    }
}