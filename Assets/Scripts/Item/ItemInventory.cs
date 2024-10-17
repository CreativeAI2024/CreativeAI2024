using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemInventory", menuName = "ScriptableObject/Item/ItemInventory")]
public class ItemInventory : ScriptableObject
{
    [SerializeField] private List<Item> itemList;
    private Dictionary<string, Item> itemDict;
    public void Initialize()
    {
        itemDict = new Dictionary<string, Item>();
        foreach (Item item in itemList)
        {
            itemDict.Add(item.ItemName, item);
        }
    }

    public Item GetItem(string Item)
    {
        return itemDict[Item];
    }
    public bool IsContains(Item searchedItem)
    {
        return itemDict.ContainsValue(searchedItem);
    }
    public IEnumerable<Item> GetItems()
    {
        return itemDict.Values;
    }
    public void Add(Item item)
    {
        if (itemDict.ContainsValue(item))
        {
            item.IncrementCount();
        }
        else
        {
            Item addedItem = Resources.Load<Item>("Items/" + item.ItemName);
            itemDict.Add(addedItem.ItemName, addedItem);
        }
    }

    public void Remove(Item item)
    {
        if (item.Count > 1)
        {
            item.DecrementCount();
        }
        else
        {
            itemDict.Remove(item.ItemName);
        }
    }
}