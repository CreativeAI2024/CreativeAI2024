using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : DontDestroySingleton<MenuUIManager>
{
    [SerializeField] private ItemInventory itemInventory;
    public Item GetItem(string item)
    {
        return itemInventory.GetItem(item);
    }

    public bool IsContains(Item searchedItem)
    {
        return itemInventory.IsContains(searchedItem);
    }

    public IEnumerable<Item> GetItems()
    {
        return itemInventory.GetItems();
    }

    public bool HasAnyPairIngredients(Item baseItem)
    {
        return itemInventory.HasAnyPairIngredients(baseItem);
    }

    public void Add(Item item)
    {
        itemInventory.Add(item);
    }

    public void Remove(Item item)
    {
        itemInventory.Remove(item);
    }

    public void TryCombine(Item item)
    {
        itemInventory.TryCombine(item);
    }

    public void PrintInventory()
    {
        string returnText = "inventory: ";
        foreach (Item item in GetItems())
        {
            returnText += item.ItemName+",  ";
        }
        DebugLogger.Log(returnText.Substring(0, returnText.Length-3));
    }
}
