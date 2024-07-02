using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class LoadItemList : MonoBehaviour
{
    [SerializeField] private ItemList itemList;
    [SerializeField] private GameObject itemButtonPrefab;
    private Dictionary<string, int> itemNumbers;

    void Start()
    {
        itemNumbers = new Dictionary<string, int>();
        foreach (Transform child in transform)
        {
            //ゲーム開始時からItemWindowがもっているボタンをitemNumbersに入れる
            //このやり方だとゲーム開始時に同じアイテムを複数持たせられない
            //↓
            //初期アイテムはItemListに入れる
            //ここではItemList内の初期アイテムをメソッドをそのまま使ってボタンを生成し、itemNumbersにも登録する
            //ItemListを、Inspectorでアイテムとその個数を登録できるようにする。配列をSerializeすれば複数登録できる？
            GameObject button = child.gameObject;
            button.tag = button.transform.GetChild(0).GetComponent<TextMeshPro>().text;
            itemNumbers.Add(getButtonName(button), 1);
        }
    }


    public void AddItemToWindow(BaseItem item)
    {
        string itemName = item.GetItemName();
        if (itemNumbers.ContainsKey(itemName))
        {
            itemNumbers[itemName]++;
        }
        else
        {
            MakeItemButton(item);
            itemNumbers.Add(itemName, 1);
        }
    }
    public void RemoveItemFromWindow(BaseItem item)
    {
        string itemName = item.GetItemName();
        if (itemNumbers.ContainsKey(itemName))
        {
            itemNumbers[itemName]--;
        }
        else
        {
            DestroyItemButton(item);
            itemNumbers.Remove(itemName);
        }
    }

    private void MakeItemButton(BaseItem item)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, transform);
        itemButton.tag = item.GetItemName();
        setButtonName(itemButton, item.GetItemName());
    }
    private void DestroyItemButton(BaseItem item)
    {
        GameObject removedItemButton = GameObject.FindWithTag(item.GetItemName());
        Destroy(removedItemButton);
    }
    private string getButtonName(GameObject button)
    {
        return button.transform.GetChild(0).GetComponent<TextMeshPro>().text;
    }
    private void setButtonName(GameObject button, string buttonName)
    {
        button.transform.GetChild(0).GetComponent<TextMeshPro>().text = buttonName;
    }
}