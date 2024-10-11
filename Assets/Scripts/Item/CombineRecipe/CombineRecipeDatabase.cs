using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CombineRecipeDatabase", menuName = "ScriptableObject/Item/CombineRecipeDatabase")]
public class CombineRecipeDatabase : ScriptableObject
{
  [SerializeField] private List<CombineRecipe> recipes;
  private Dictionary<Item, Item> pairItemDictionary;
  private Dictionary<Item, Item> createdItemDictionary;
  public void Initialize()
  {
    pairItemDictionary = new Dictionary<Item, Item>();
    createdItemDictionary = new Dictionary<Item, Item>();
    foreach (CombineRecipe recipe in recipes)
    {
      pairItemDictionary.Add(recipe.Material1, recipe.Material2);
      pairItemDictionary.Add(recipe.Material2, recipe.Material1);
      createdItemDictionary.Add(recipe.Material1, recipe.CreatedItem);
      createdItemDictionary.Add(recipe.Material2, recipe.CreatedItem);
    }
  }
  public Item GetPairItem(Item materialItem)
  {
    try
    {
      return pairItemDictionary[materialItem];
    }
    catch (System.Exception)
    {
      return null;
    }

  }
  public Item GetCreatedItem(Item materialItem)
  {
    try
    {
      return createdItemDictionary[materialItem];
    }
    catch (System.Exception)
    {
      return null;
    }
  }
}
