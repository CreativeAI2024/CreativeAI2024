using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CombineRecipeList", menuName = "ScriptableObject/Item/CombineRecipeList")]
public class CombineRecipeList : ScriptableObject
{
  [SerializeField] private List<CombineRecipe> recipes;
  public List<CombineRecipe> Recipes => recipes;
  public void SetIsCombinable()
  {
    foreach (CombineRecipe recipe in recipes)
    {
      recipe.Material1.IsCombinable = true;
      recipe.Material2.IsCombinable = true;
    }
  }
  public BaseItem GetPairItem(string itemName)
  {
    foreach (CombineRecipe recipe in recipes)
    {
      if (recipe.Material1.ItemName == itemName)
      {
        return recipe.Material2;
      }
      else if (recipe.Material2.ItemName == itemName)
      {
        return recipe.Material1;
      }
    }
    return null;
  }
  public BaseItem GetCreatedItem(string materialName)
  {
    foreach (CombineRecipe recipe in recipes)
    {
      if (recipe.Material1.ItemName == materialName || recipe.Material2.ItemName == materialName)
      {
        return recipe.CreatedItem;
      }
    }
    return null;
  }
}
