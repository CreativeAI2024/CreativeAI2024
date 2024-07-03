using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemList", menuName = "CreateItemList")]
public class ItemList : ScriptableObject
{
    public List<BaseItem> Items { get; set; } = new List<BaseItem>();

    public BaseItem Search(string searchedItemName)
    {
        foreach (BaseItem item in Items)
        {
            if (item.ItemName == searchedItemName)
            {
                return item;
            }
        }
        return null;
    }

    public void CheckDuplication()
    {
        HashSet<string> checkerSet = new HashSet<string>();
        foreach (BaseItem item in Items)
        {
            if (!checkerSet.Add(item.ItemName))
            {
                throw new ArgumentException("ItemList内で" + item + "が重複しています。");
            }
        }
    }

    public void Add(BaseItem item)
    {
        BaseItem addedItem = Search(item.ItemName);
        if (addedItem == null)
        {
            Items.Add(item);
        }
        else
        {
            addedItem.IncrementCount();
        }
    }

    public void Remove(BaseItem item)
    {
        BaseItem removedItem = Search(item.ItemName);
        if (removedItem.Count > 1)
        {
            removedItem.DecrementCount();
        }
        else
        {
            Items.Remove(removedItem);
        }
    }
}