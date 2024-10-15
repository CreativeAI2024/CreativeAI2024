using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CombineRecipeDatabase", menuName = "ScriptableObject/Item/CombineRecipeDatabase")]
public class CombineRecipeDatabase : ScriptableObject
{
    [SerializeField] private List<CombineRecipe> recipes;
    private Dictionary<Item, Item> pairIngredientDict;
    private Dictionary<Item, Item> resultItemDict;
    public void Initialize()
    {
        pairIngredientDict = new Dictionary<Item, Item>();
        resultItemDict = new Dictionary<Item, Item>();
        foreach (CombineRecipe recipe in recipes)
        {
            pairIngredientDict.Add(recipe.IngredientA, recipe.IngredientB);
            pairIngredientDict.Add(recipe.IngredientB, recipe.IngredientA);
            resultItemDict.Add(recipe.IngredientA, recipe.ResultItem);
            resultItemDict.Add(recipe.IngredientB, recipe.ResultItem);
        }
    }
    public Item GetPairIngredient(Item ingredientItem)
    {
        if (pairIngredientDict.ContainsKey(ingredientItem))
        {
            return pairIngredientDict[ingredientItem];
        }
        else
        {
            return null;
        }
    }
    public Item GetResultItem(Item ingredientItem)
    {
        if (resultItemDict.ContainsKey(ingredientItem))
        {
            return resultItemDict[ingredientItem];
        }
        else
        {
            return null;
        }
    }
}
