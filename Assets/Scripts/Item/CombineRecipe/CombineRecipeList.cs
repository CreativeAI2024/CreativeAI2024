using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CombineRecipeList", menuName = "ScriptableObject/Item/CombineRecipeList")]
public class CombineRecipeList : ScriptableObject
{
  [SerializeField] private List<CombineRecipe> recipes;
  private Dictionary<string, BaseItem> pairItemDictionary;
  private Dictionary<string, BaseItem> createdItemDictionary;
  public void Setup()
  {
    MakeDictionaries();
    SetIsCombinable();
  }
  private void MakeDictionaries()
  {
    pairItemDictionary = new Dictionary<string, BaseItem>();
    createdItemDictionary = new Dictionary<string, BaseItem>();
    foreach (CombineRecipe recipe in recipes)
    {
      pairItemDictionary.Add(recipe.Material1.ItemName, recipe.Material2);
      pairItemDictionary.Add(recipe.Material2.ItemName, recipe.Material1);
      createdItemDictionary.Add(recipe.Material1.ItemName, recipe.CreatedItem);
      createdItemDictionary.Add(recipe.Material2.ItemName, recipe.CreatedItem);
    }
  }
  private void SetIsCombinable()
  {
    foreach (CombineRecipe recipe in recipes)
    {
      recipe.Material1.IsCombinable = true;
      recipe.Material2.IsCombinable = true;
    }
  }
  public BaseItem GetPairItem(string materialName)
  {
    try
    {
      return pairItemDictionary[materialName];
    }
    catch (System.Exception)
    {
      Debug.Log(materialName + "'s pair item is not found.");
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
