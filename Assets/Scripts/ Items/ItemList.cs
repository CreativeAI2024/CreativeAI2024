using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="ItemList", menuName ="CreateItemList")]
public class ItemList : ScriptableObject
{
    private List<BaseItem> itemlist;
    public List<BaseItem> GetItemList() => itemlist;
    public BaseItem FindItem(BaseItem searchedItem)
    {
        foreach (BaseItem item in itemlist)
        {
            if (item==searchedItem)
            {
                return item;
            }
        }
        return null;
    }

    public void AddItem(BaseItem AddedItem)
    {
        itemlist.Add(AddedItem);
    }
    public void RemoveItem(BaseItem RemovedItem)
    {
        itemlist.Remove(RemovedItem);
    }

}
