using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemList", menuName = "ScriptableObject/Item/ItemList")]
public class ItemList : ScriptableObject
{
    [SerializeField] private List<BaseItem> items;
    public List<BaseItem> Items 
    { 
        get { return items; }
    }
    public BaseItem Search(string searchedItemName)
    {
        foreach (BaseItem item in items)
        {
            if (item.ItemName == searchedItemName)
            {
                return item;
            }
        }
        return null;
    }

    private void CheckDuplication()
    {
        HashSet<string> checkerSet = new();
        foreach (BaseItem item in items)
        {
            if (!checkerSet.Add(item.ItemName))
            {
                throw new InvalidOperationException ("ItemList内で" + item + "が重複しています。");
            }
        }
    }

    public void Add(string itemName) 
    {
        BaseItem addedItem = Search(itemName);
        if (addedItem == null)
        {
            items.Add(addedItem);
        }
        else
        {
            addedItem.IncrementCount();
        }
        CheckDuplication();
    }

    public void Remove(string itemName)
    {
        BaseItem removedItem = Search(itemName);
        if (removedItem.Count > 1)
        {
            removedItem.DecrementCount();
        }
        else
        {
            items.Remove(removedItem);
        }
        CheckDuplication();
    }
}