using UnityEngine;
using UnityEngine.EventSystems;

public class CombineItems : MonoBehaviour
{
    private InputSetting _inputSetting;
    private ItemInventory itemInventory;
    private CombineRecipeDatabase combineRecipeDatabase;
    private BaseItem materialItem;
    public BaseItem MaterialItem {
        set {materialItem = value;}
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
    BaseItem pairItem = combineRecipeDatabase.GetPairItem(materialItem); //error
    if (itemInventory.IsContains(pairItem)) //ペアアイテムを持ってるか。引数：渡されたthisItem→recipeから取得
    {
      itemInventory.Add(combineRecipeDatabase.GetCreatedItem(materialItem));
      itemInventory.Remove(materialItem);
      itemInventory.Remove(pairItem);
    } 
  }
}