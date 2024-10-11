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
    private Dictionary<string, Item> itemDictionary;
    public void Initialize()
    {
        itemSet = new HashSet<Item>();
        itemDictionary = new Dictionary<string, Item>();
        foreach (Item item in itemList)
        {
            if (itemSet.Add(item))
            {
                itemDictionary.Add(item.ItemName, item);
            }
        }
    }

    public Item GetItem(string Item)
    {
        try
        {
            return itemDictionary[Item];
        }
        catch
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
            itemDictionary.Add(addedItem.ItemName, addedItem);
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
            itemDictionary.Remove(item.ItemName);
        }
    }
}