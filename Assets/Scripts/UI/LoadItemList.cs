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
        int index = GetIndexByButton(item.GetItemName());
        if (index == -1)
        {
            itemButtons.Add((MakeItemButton(item), 1));
        }
        //新しいアイテムならMakeItemButton()、すでにあるアイテムなら、個数を表す整数を増やす
        
    }
    public void RemoveItemFromWindow(BaseItem item)
    {
        DestroyItemButton(item);
    }

    private int GetIndexByButton(String searchedButtonName)
    {
        for (int i=0; i<itemButtons.Count; i++)
        {
            if (itemButtons[i].button.GetComponent<TextMeshPro>().text.Equals(searchedButtonName))
            {
                return i;
            }
        }
        return -1;
    }

    private void MakeItemButton(BaseItem item)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab,transform);
        itemButton.transform.GetChild(0).GetComponent<TextMeshPro>().text = item.GetItemName();
    }
    private void DestroyItemButton(BaseItem item)
    {
        (GameObject, int) removedItemButton = FindInItemButtons(item.GetItemName());
        Destroy(removedItemButton);
    }
}