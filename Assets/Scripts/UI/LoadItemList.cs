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
    private Dictionary<String, int> itemNumbers;

    void Start()
    {
        itemNumbers = new Dictionary<string, int>();
    }


    public void AddItemToWindow(BaseItem item)
    {
        String itemName = item.GetItemName();
        if(itemNumbers.ContainsKey(itemName))
        {
            itemNumbers[itemName] ++;
        }
        else 
        {
            MakeItemButton(item);
            itemNumbers.Add(itemName, 1);
        }
    }
    public void RemoveItemFromWindow(BaseItem item)
    {
        String itemName = item.GetItemName();
        if(itemNumbers.ContainsKey(itemName))
        {
            itemNumbers[itemName] --;
        }
        else 
        {
            DestroyItemButton(item);
            itemNumbers.Remove(itemName);
        }
    }

    private void MakeItemButton(BaseItem item)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab,transform);
        itemButton.tag = item.GetItemName();
        itemButton.transform.GetChild(0).GetComponent<TextMeshPro>().text = item.GetItemName();
    }
    private void DestroyItemButton(BaseItem item)
    {
        GameObject removedItemButton = GameObject.FindWithTag(item.GetItemName());
        Destroy(removedItemButton);
    }
}