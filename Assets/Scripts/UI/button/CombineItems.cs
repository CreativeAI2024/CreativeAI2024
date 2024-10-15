using UnityEngine;
using UnityEngine.EventSystems;

public class CombineItems : MonoBehaviour
{
    private InputSetting _inputSetting;
    private ItemInventory itemInventory;
    private CombineRecipeDatabase combineRecipeDatabase;
    private Item materialItem;
    public Item MaterialItem
    {
        set { materialItem = value; }
    }
    void Start()
    {
        _inputSetting = InputSetting.Load();
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (_inputSetting.GetDecideKeyDown())
            {
                Combine();
            }
        }
    }

    private void Combine()
    {
        Debug.Log("Combine() called");
        Item pairItem = combineRecipeDatabase.GetPairIngredient(materialItem); //error
        if (itemInventory.IsContains(pairItem)) //ペアアイテムを持ってるか。引数：渡されたthisItem→recipeから取得
        {
            itemInventory.Add(combineRecipeDatabase.GetResultItem(materialItem));
            itemInventory.Remove(materialItem);
            itemInventory.Remove(pairItem);
        }
    }
}