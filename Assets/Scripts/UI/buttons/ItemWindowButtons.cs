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
//ItemWindowはButtons内のItemにfor文で全てにアクセスできる
//引数の型がItemでも、実際に引数として渡される変数がそれを継承したImageShowItemクラスなら、ImageShowItemクラスとして認識される

public class ItemButtons : MonoBehaviour
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private GameObject itemImageScreen;
    private GameObject itemWindow;
    [SerializeField] private GameObject confirmWindow;

    void OnEnable()
    {
        itemWindow = transform.parent.gameObject;
        LoadItemInventory();
    }

    private void LoadItemInventory()
    {
        foreach (Transform child in transform)
        {
            Debug.Log(child.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            if (!itemInventory.GetItem(child.GetChild(0).GetComponent<TextMeshProUGUI>().text)) //アイテム一覧のボタンにあってアイテムリストにないボタンを確かめる
            {
                Destroy(child.gameObject);
            }
        }
        foreach (Item item in itemInventory.GetItems())
        {
            if (!Search(item.ItemName))
            {
                MakeItemButton(item);
            }
        }
    }

    //アイテム合成の時にリストを探索する時に呼び出す
    //上の場合でも、ItemInventory内を探せばいいのでは？
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
    private void MakeItemButton(Item item)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, transform);
        SetButtonName(itemButton, item);
        if (item.Image!=null && item.Text.Count!=0)
        {
            itemButton.AddComponent<ImageTextButton>();
        }
        else if (item.Image!=null)
        {
            itemButton.AddComponent<ImageButton>();
        }
        else if (item.Text.Count!=0)
        {
            itemButton.AddComponent<TextButton>();
        }
        if (combineRecipeDatabase.GetPairItem(item))
        {
            itemButton.AddComponent<CombineButton>();
        }
    }


    private string GetButtonName(GameObject button)
    {
        return button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }

    private void SetButtonName(GameObject button, Item buttonItem)
    {
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonItem.ItemName;
    }
}