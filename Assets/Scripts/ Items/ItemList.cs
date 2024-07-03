using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemList", menuName = "CreateItemList")]
public class ItemList : ScriptableObject
{
    [SerializeField] private List<BaseItem> initialItems;
    [SerializeField] private List<int> initialQuantities;
    [SerializeField] private LoadItemList loadItemList;

    public List<Tuple<BaseItem, int>> GetItemAndQuantilyList()
    {
        List<Tuple<BaseItem, int>> rtn = new List<Tuple<BaseItem, int>>();
        for (int i=0; i<initialItems.Count; i++)
        {
            rtn.Add(new Tuple<BaseItem, int>(initialItems[i], initialQuantities[i]));
        }
        return rtn;
    }
    public BaseItem FindItem(BaseItem searchedItem)
    {
        foreach (BaseItem item in initialItems)
        {
            if (item == searchedItem)
            {
                return item;
            }
        }
        return null;
    }
    public void AddItem(BaseItem AddedItem)
    {
        initialItems.Add(AddedItem);
        loadItemList.AddItemToWindow(AddedItem);

    }
    public void RemoveItem(BaseItem RemovedItem)
    {
        initialItems.Remove(RemovedItem);
        loadItemList.RemoveItemFromWindow(RemovedItem);
    }
    public void CheckDuplication()
    {
        HashSet<string> checkerSet = new HashSet<string>();
        foreach (BaseItem item in initialItems)
        {
            if (checkerSet.Add(item.GetItemName()) == false)
            {
                Debug.Log("ItemList内で" + item + "が重複しています。");
            }
        }
    }
}
