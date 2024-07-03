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

    void Awake()
    {
        itemNumbers = new Dictionary<string, int>();
        foreach (Tuple<BaseItem, int> itemAndQuantity in itemList.GetItemAndQuantilyList())
        {
            AddItemToWindow(itemAndQuantity.Item1);
            itemNumbers.Add(itemAndQuantity.Item1.GetItemName(), itemAndQuantity.Item2);
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