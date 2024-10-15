using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemInventory", menuName = "ScriptableObject/Item/ItemInventory")]
public class ItemInventory : ScriptableObject
{
    [SerializeField] private List<Item> itemList;
    private HashSet<Item> itemSet;
    private Dictionary<string, Item> itemDict;
    public void Initialize()
    {
        itemSet = new HashSet<Item>();
        itemDict = new Dictionary<string, Item>();
        foreach (Item item in itemList)
        {
            if (itemSet.Add(item))
            {
                itemDict.Add(item.ItemName, item);
            }
        }
    }

    public Item GetItem(string Item)
    {
        if (itemDict.ContainsKey(Item))
        {
            return itemDict[Item];
        }
        else
        {
            return null;
        }
    }
    public bool IsContains(Item searchedItem)
    {
        return itemSet.Contains(searchedItem);
    }
    public ReadOnlyCollection<Item> GetItems()
    {
        return itemList.AsReadOnly();
    }
    public void Add(Item item)
    {
        if (itemSet.Contains(item))
        {
            item.IncrementCount();
        }
        else
        {
            Item addedItem = Resources.Load<Item>("Items/" + item.ItemName);
            itemList.Add(addedItem);
            itemSet.Add(addedItem);
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
            itemList.Remove(item);
            itemSet.Remove(item);
            itemDict.Remove(item.ItemName);
        }
    }
}