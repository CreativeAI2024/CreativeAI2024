using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemInventory", menuName = "ScriptableObject/Item/ItemInventory")]
public class ItemInventory : ScriptableObject
{
    [SerializeField] private List<BaseItem> itemList;
    private HashSet<BaseItem> itemSet;
    private Dictionary<string, BaseItem> itemDictionary;
    public List<BaseItem> Items => itemList;
    public void Initialize()
    {
        itemSet = new HashSet<BaseItem>();
        itemDictionary = new Dictionary<string, BaseItem>();
        foreach(BaseItem item in itemList)
        {
            if (itemSet.Add(item))
            {
                itemDictionary.Add(item.ItemName, item);
            }
        }
    }

    public BaseItem GetItem(string baseItem)
    {
        try
        {
            return itemDictionary[baseItem];
        }
        catch
        {
            return null;
        }
    }
    public bool IsContains(BaseItem searchedItem)
    {
        return itemSet.Contains(searchedItem);
    }
    public void Add(BaseItem item) 
    {
        if (itemSet.Contains(item))
        {
            item.IncrementCount();
        }
        else
        {
            BaseItem addedItem = Resources.Load<BaseItem>("Items/"+item.ItemName);
            itemList.Add(addedItem);
            itemSet.Add(addedItem);
            itemDictionary.Add(addedItem.ItemName, addedItem);
        }
    }

    public void Remove(BaseItem item)
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