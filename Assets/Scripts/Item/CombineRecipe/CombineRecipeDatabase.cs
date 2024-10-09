using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CombineRecipeDatabase", menuName = "ScriptableObject/Item/CombineRecipeDatabase")]
public class CombineRecipeDatabase : ScriptableObject
{
  [SerializeField] private List<CombineRecipe> recipes;
  private Dictionary<BaseItem, BaseItem> pairItemDictionary;
  private Dictionary<BaseItem, BaseItem> createdItemDictionary;
  public void Initialize()
  {
    pairItemDictionary = new Dictionary<BaseItem, BaseItem>();
    createdItemDictionary = new Dictionary<BaseItem, BaseItem>();
    foreach (CombineRecipe recipe in recipes)
    {
      pairItemDictionary.Add(recipe.Material1, recipe.Material2);
      pairItemDictionary.Add(recipe.Material2, recipe.Material1);
      createdItemDictionary.Add(recipe.Material1, recipe.CreatedItem);
      createdItemDictionary.Add(recipe.Material2, recipe.CreatedItem);
    }
  }
  public BaseItem GetPairItem(BaseItem materialItem)
  {
    try
    {
      return pairItemDictionary[materialItem];
    }
    catch (System.Exception)
    {
      Debug.Log(materialItem + "'s pair item is not found.");
      throw;
    }

  }
  public BaseItem GetCreatedItem(string materialName)
  {
    try
    {
      return createdItemDictionary[materialName];
    }
    catch (System.Exception)
    {
      Debug.Log(materialName + "'s created item is not found.");
      throw;
    }
  }
}
