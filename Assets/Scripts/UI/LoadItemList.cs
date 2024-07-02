using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class LoadItemList : MonoBehaviour
{
    [SerializeField] private ItemList itemList;
    [SerializeField] private GameObject itemButtonPrefab;
    private List<(GameObject button, int count)> itemButtons; //int型は個数を表す

    void Start()
    {
        itemButtons = new List<(GameObject button, int count)>();
    }


    public void AddItemToWindow(BaseItem item)
    {
        MakeItemButton(item);
        //新しいアイテムならMakeItemButton()、すでにあるアイテムなら、個数を表す整数を増やす
        ValueTuple<GameObject, int> addedItem;
        if ((addedItem = FindInItemButtons(item.GetItemName())) != (null, -1))
        {

        }
    }
    public void RemoveItemFromWindow(BaseItem item)
    {
        DestroyItemButton(item);
    }

    private (GameObject, int) FindInItemButtons(String searchedButtonName) //FindWithTag()の代わり
    {
        foreach ((GameObject button, int count) in itemButtons)
        {
            if (button.GetComponent<TextMeshPro>().text==searchedButtonName)
            {
                return (button, count);
            }
        }
        return (null, -1);
    }

    private void MakeItemButton(BaseItem item)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab,transform);
        itemButton.transform.GetChild(0).GetComponent<TextMeshPro>().text = item.GetItemName();
    }
    private void DestroyItemButton(BaseItem item)
    {
        GameObject removedItemButton = GameObject.FindWithTag(item.GetItemName());
        Destroy(removedItemButton);
    }
}