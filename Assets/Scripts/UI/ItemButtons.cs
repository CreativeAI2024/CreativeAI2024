using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//TODO: アイテムの機能作る
//TODO: カーソル移動ロック機能作る→今のところ不要になった
//TODO: アイテム画像は1600*900で出してもらうことを視覚班に伝える→ゲーム全体の解像度によるので保留
//TODO: ItemButtonに個数表示機能
//TODO: デバッグ
//TODO: メニューUIの構造の見直し（自分以外でも使えるように）
//TODO:     スクリプトをどのオブジェクトにアタッチすれば良いか
//TODO:     プレハブを実体化すればすぐメニューUIが使えるようにする
//TODO:     SerializeFieldは本当にこのスクリプトで必要か
public class ItemButtons : MonoBehaviour
{
    [SerializeField] private ItemList itemList;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private GameObject itemImageScreen;
    private GameObject itemWindow;
    [SerializeField] private GameObject confirmWindow;
    private GameObject confirmYesButton;


    void OnEnable()
    {
        itemWindow = transform.parent.gameObject;
        confirmYesButton = confirmWindow.transform.GetChild(0).gameObject;
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
        foreach (BaseItem item in itemList.Items) {
            if (Search(item.ItemName))
            {
            }
            else
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

    public void Add(string itemName) //アイテムをゲットした時に
    {
        itemList.Add(itemName);
        GameObject addedButton = this.Search(itemName);
        if (addedButton == null)
        {
            MakeItemButton(itemName);
        }
        else
        {
            itemList.Search(itemName).IncrementCount();
        }
        CheckDuplication();
    }

    public void Remove(string itemName) //消耗品のアイテム等を使った時に
    {
        itemList.Remove(itemName);
        GameObject removedButton = this.Search(itemName);
        if (removedButton == null)
        {
        }
        else if (itemList.Search(itemName).Count > 1)
        {
            itemList.Search(itemName).DecrementCount();
        }
        else
        {
            Destroy(removedButton);
            itemList.Remove(itemName);
        }
        CheckDuplication();
    }

    private void MakeItemButton(string itemName)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, transform);
        SetButtonName(itemButton, itemName);
        switch (itemList.Search(itemName))
        {
            case ImageShowCombineMaterialItem:
                itemButton.AddComponent<OpenWindow>();
                itemButton.GetComponent<OpenWindow>().currentWindow = itemWindow;
                itemButton.GetComponent<OpenWindow>().nextWindow = itemImageScreen;

                itemImageScreen.GetComponent<OpenWindow>().nextWindow = itemWindow;
                itemButton.AddComponent<SetEffectImageShowCombineMaterial>();
                break;
            case ImageShowItem:
                itemButton.AddComponent<OpenWindow>();
                itemButton.GetComponent<OpenWindow>().currentWindow = itemWindow;
                itemButton.GetComponent<OpenWindow>().nextWindow = itemImageScreen;
                itemImageScreen.GetComponent<OpenWindow>().nextWindow = itemWindow;
                itemButton.AddComponent<SetEffectImageShow>();
                break;
            case CombineMaterialItem:
                itemButton.AddComponent<OpenWindow>();
                itemButton.GetComponent<OpenWindow>().enabled = false;
                itemButton.GetComponent<OpenWindow>().currentWindow = itemWindow;
                itemButton.GetComponent<OpenWindow>().nextWindow = confirmWindow;
                itemButton.AddComponent<SetEffectCombineMaterial>();
                break;
        }
        
    }

    //TODO: アイテム周りのクラス図を作る
    //TODO: 現在の問題：Buttonからアイテムの画像にアクセスできない
    //ImageShowItem Objectは画像と、そのgetterを持っている
    //ItemWindowはButtons内のBaseItemにfor文で全てにアクセスできる
    //引数の型がBaseItemでも、実際に引数として渡される変数がそれを継承したImageShowItemクラスなら、ImageShowItemクラスとして認識される
    //ItemButtonはアイテム名しか持っていない
    //ItemButtonからItemにアクセスできるようにして、ItemButtonを押した時にさまざまなことができるようにしたい
    //調べてみたら、ItemList.Search(ItemName)でアクセスできる！

    private string GetButtonName(GameObject button)
    {
        return button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }

    private void SetButtonName(GameObject button, string buttonName)
    {
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonName;
    }
}