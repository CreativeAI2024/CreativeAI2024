using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemInventory", menuName = "ScriptableObject/Item/ItemInventory")]
public class ItemInventory : ScriptableObject
{
    [SerializeField] private List<Item> itemList;
    private Dictionary<string, Item> itemDict;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
    public void Initialize()
    {
        itemDict = new Dictionary<string, Item>();
        foreach (Item item in itemList)
        {
            itemDict.Add(item.ItemName, item);
        }
    }

    public Item GetItem(string item)
    {
        return itemDict[item];
    }
    public bool IsContains(Item searchedItem)
    {
        return itemDict.ContainsValue(searchedItem);
    }
    public IEnumerable<Item> GetItems()
    {
        return itemDict.Values;
    }
    public bool HasAnyPairIngredients(Item baseItem)
    {
        if (combineRecipeDatabase.IsCombinable(baseItem))
        {
            return itemDict.Values.Any(item => combineRecipeDatabase.GetPairIngredients(baseItem).Contains(item));
        }
        return false;
    }
    public void Add(Item item)
    {
        if (itemDict.ContainsValue(item))
        {
            item.IncrementCount();
        }
        else
        {
            itemDict.Add(item.ItemName, item);
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

    public void TryCombine(Item item)
    {
        Item pairItem = combineRecipeDatabase.GetPairIngredients(item)[0];
        if (IsContains(pairItem))
        {
            Remove(item);
            Remove(pairItem);
            Add(combineRecipeDatabase.GetResultItem(item, pairItem));
        }
    }
}