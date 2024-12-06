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
    public void Initialize(IEnumerable<Item> items)
    {
        itemDict = new Dictionary<string, Item>();
        foreach (Item item in items)
        {
            itemDict.Add(item.name, item);
        }
    }
    
    public void Initialize()
    {
        itemDict = new Dictionary<string, Item>();
        foreach (Item item in itemList)
        {
            itemDict.Add(item.name, item);
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
        itemDict.Add(item.name, item);
        if (item.name.Equals("Rei'sBlood") || item.name.Equals("SthFlesh") || item.name.Equals("BugsInJar"))
        {
            DebugLogger.Log((FlagManager.Instance.ReiStatus + 1), DebugLogger.Colors.Yellow);
            FlagManager.Instance.SetReiStatus(FlagManager.Instance.ReiStatus + 1);
        }
        SaveInventory();
    }

    public void Remove(Item item)
    {
        itemDict.Remove(item.name);
        SaveInventory();
    }
    
    private void SaveInventory()
    {
        InventoryData saveInventoryData = new InventoryData
        {
            Items = itemDict.Keys.ToList()
        };
        string itemSaveFilePath = string.Join('/', Application.persistentDataPath, "Inventory.dat");
        SaveUtility.DataToSaveFile(saveInventoryData, itemSaveFilePath);
    }

    public void TryCombine(Item item)
    {
        List<Item> pairIngredients = combineRecipeDatabase.GetPairIngredients(item);
        if (!pairIngredients.Any()) return;
        Item pairItem = pairIngredients[0];
        if (IsContains(pairItem))
        {
            Remove(item);
            Remove(pairItem);
            Add(combineRecipeDatabase.GetResultItem(item, pairItem));
        }
    }
    public void PrintInventory()
    {
        string returnText = "inventory: ";
        foreach (Item item in GetItems())
        {
            returnText += item.ItemName + ",  ";
        }
        DebugLogger.Log(returnText.Substring(0, returnText.Length - 3));
    }
}