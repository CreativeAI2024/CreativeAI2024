using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombineItems
{
    private ItemInventory itemInventory;
    private CombineRecipeDatabase combineRecipeDatabase;
    private Item materialItem;
    public CombineItems(Item materialItem)
    {
        this.materialItem = materialItem;
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
    }

    public void Combine(Item pairItem)
    {
        Debug.Log("Combine() called");
        if (itemInventory.IsContains(pairItem))
        {
            //pairItemがコレクションだからエラー出てる
            itemInventory.Remove(materialItem);
            itemInventory.Remove(pairItem);
            itemInventory.Add(combineRecipeDatabase.GetResultItem(materialItem, pairItem));
        }
    }
    
    public bool HasPairItemInInventory(Item item)
    {
        List<Item> pairIngredients = combineRecipeDatabase.GetPairIngredients(item);
        return pairIngredients.Any(x => itemInventory.IsContains(x));
    }
}