using System.Collections.Generic;
using UnityEngine;

public class CombineItems : IDecideCancelObject
{
    private ItemInventory itemInventory;
    private CombineRecipeDatabase combineRecipeDatabase;
    private Item materialItem;
    public Item MaterialItem
    {
        set { materialItem = value; }
    }
    public CombineItems()
    {
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
    }

    public void Combine()
    {
        Debug.Log("Combine() called");
        HashSet<Item> pairItem = combineRecipeDatabase.GetPairIngredients(materialItem);
        if (itemInventory.IsContains(pairItem))
        {
            //pairItemがコレクションだからエラー出てる
            // itemInventory.Add(combineRecipeDatabase.GetResultItem(materialItem, pairItem));
            itemInventory.Remove(materialItem);
            // itemInventory.Remove(pairItem);
        }
    }

    public void OnDecideKeyDown()
    {
        Combine();
    }
    public void OnCancelKeyDown()
    {}
}