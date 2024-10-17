using UnityEngine;

public class CombineItems : IFocusedObject
{
    private ItemInventory itemInventory;
    private CombineRecipeDatabase combineRecipeDatabase;
    private Item materialItem;
    public Item MaterialItem
    {
        set { materialItem = value; }
    }
    CombineItems()
    {
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
    }

    public void Combine()
    {
        Debug.Log("Combine() called");
        Item pairItem = combineRecipeDatabase.GetPairIngredient(materialItem);
        if (itemInventory.IsContains(pairItem))
        {
            itemInventory.Add(combineRecipeDatabase.GetResultItem(materialItem));
            itemInventory.Remove(materialItem);
            itemInventory.Remove(pairItem);
        }
    }

    public void OnDecideKeyDown()
    {
        Combine();
    }
}