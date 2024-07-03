using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemList", menuName = "ScriptableObject/Item/ItemList")]
public class ItemList : ScriptableObject
{
    [SerializeField] private List<BaseItem> items = new();
    public List<BaseItem> Items { get; set; }

    public BaseItem Search(string searchedItemName) //Itemを探索したいときに　これとLoadItemListのSearch()はどちらかはprivateでもいいかも？
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

    private void CheckDuplication()
    {
        HashSet<string> checkerSet = new();
        foreach (BaseItem item in Items)
        {
            if (!checkerSet.Add(item.ItemName))
            {
                throw new InvalidOperationException ("ItemList内で" + item + "が重複しています。");
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
        CheckDuplication();
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
        CheckDuplication();
    }
}