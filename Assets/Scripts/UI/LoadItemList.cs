using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        SetButtonName(itemButton, item.GetItemName());
    }
    private void DestroyItemButton(BaseItem item)
    {
        foreach (GameObject button in transform)
        {
            if (GetButtonName(button).Equals(item.GetItemName()))
            {
                Destroy(button);
            }
        }
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