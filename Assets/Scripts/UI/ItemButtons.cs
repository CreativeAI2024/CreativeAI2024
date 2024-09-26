using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
//TODO: 合成機能作る
//TODO: アイテムの機能作る
//TODO: カーソル移動ロック機能作る→今のところ不要になった
//TODO: 画像表示中にカーソル移動できないようにする
//TODO: アイテム画像は1600*900で出してもらうことを視覚班に伝える
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
    [SerializeField] private GameObject confirmWindow;

    void OnEnable()
    {
        Debug.Log("ItemButtons on "+transform.parent.gameObject);
        LoadItemList();
    }

    private void LoadItemList()
    {
        foreach (Transform child in transform)
        {
            if (!itemList.Search(child.GetChild(0).GetComponent<TextMeshProUGUI>().text))
            {
                Destroy(child.gameObject);
                Debug.Log("Destroyed "+child.gameObject);
            }
        }
        foreach (BaseItem item in itemList.Items) {
            if (Search(item.ItemName))
            {
                Debug.Log(item.ItemName+" Button already exist");
            }
            else
            {
                Debug.Log(item.ItemName+" Button hasn't exist yet");
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
        //この方法でのアイテムの型判別は、子クラスも通してしまう。caseの順番を気をつければ今のところ大丈夫だけど、、、
        switch (itemList.Search(itemName))
        {
            case ImageShowItem :
                itemButton.AddComponent<OpenWindow>();
                itemButton.GetComponent<OpenWindow>().currentWindow = transform.parent.gameObject;
                itemButton.GetComponent<OpenWindow>().nextWindow = itemImageScreen;
                itemButton.AddComponent<SetImageAndNextWindow>();
                break;
            case CombineMaterialItem:
                itemButton.AddComponent<OpenWindow>();
                itemButton.GetComponent<OpenWindow>().enabled = false;
                itemButton.GetComponent<OpenWindow>().currentWindow = transform.parent.gameObject;
                itemButton.GetComponent<OpenWindow>().nextWindow = confirmWindow;
                itemButton.AddComponent<SetCombineMaterial>();
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