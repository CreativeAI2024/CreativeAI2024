using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadItemList : MonoBehaviour
{
    [SerializeField] private ItemList itemList;
    [SerializeField] private GameObject itemButtonPrefab;

    void Awake()
    {
        foreach (BaseItem item in itemList.Items)
        {
            Add(item);
        }
    }

    //重複チェック
    //探索
    //追加
    //削除
    public GameObject Search(string searchedButtonName)
    {
        foreach (GameObject button in transform)
        {
            if (GetButtonName(button).Equals(searchedButtonName))
            {
                return button;
            }
        }
        return null;
    }
    public void CheckDuplication()
    {
        HashSet<string> checkerSet = new HashSet<string>();
        foreach (BaseItem item in itemList.Items)
        {
            if (!checkerSet.Add(item.ItemName))
            {
                throw new ArgumentException("ItemWindow内で" + item + "ボタンが重複しています。");
            }
        }
    }
    public void Add(BaseItem item)
    {
        GameObject addedButton = this.Search(item.ItemName);
        if (addedButton == null)
        {
            MakeItemButton(item);
        }
        else
        {
            itemList.Search(item.ItemName).IncrementCount();
        }
    }
    public void Remove(BaseItem item)
    {
        GameObject removedButton = this.Search(item.ItemName);
        if (removedButton == null)
        {
            Debug.Log("存在しない"+item.ItemName+"ボタンを削除しようとしています。");
        }
        else if (itemList.Search(item.ItemName).Count > 1)
        {
            itemList.Search(item.ItemName).DecrementCount();
        }
        else
        {
            Destroy(removedButton);
            itemList.Remove(item);
        }
    }
    private void MakeItemButton(BaseItem item)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, transform);
        SetButtonName(itemButton, item.ItemName);
    }
    private string GetButtonName(GameObject button)
    {
        return button.transform.GetChild(0).GetComponent<TextMeshPro>().text;
    }
    private void SetButtonName(GameObject button, string buttonName)
    {
        button.transform.GetChild(0).GetComponent<TextMeshPro>().text = buttonName;
    }
}