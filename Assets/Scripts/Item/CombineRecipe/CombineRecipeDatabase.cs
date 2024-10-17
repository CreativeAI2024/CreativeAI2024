using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CombineRecipeDatabase", menuName = "ScriptableObject/Item/CombineRecipeDatabase")]
public class CombineRecipeDatabase : ScriptableObject
{
    [SerializeField] private List<CombineRecipe> recipes;
    private Dictionary<Item, List<Item>> pairIngredientDict;
    private Dictionary<(Item, Item), Item> resultItemDict;
    public void Initialize()
    {
        pairIngredientDict = new Dictionary<Item, List<Item>>();
        resultItemDict = new Dictionary<(Item, Item), Item>();
        foreach (CombineRecipe recipe in recipes)
        {
            SetPairIngredient(recipe.IngredientA, recipe.IngredientB);
            SetPairIngredient(recipe.IngredientB, recipe.IngredientA);
            resultItemDict.Add((recipe.IngredientA, recipe.IngredientB), recipe.ResultItem);
            resultItemDict.Add((recipe.IngredientB, recipe.IngredientA), recipe.ResultItem);
        }
    }

    private void SetPairIngredient(Item item, Item pair)
    {
        if (pairIngredientDict.ContainsKey(item))
        {
            List<Item> items = pairIngredientDict[item];
            if (items == null)
            {
                items = new List<Item>();
            }
            items.Add(pair);
            pairIngredientDict.Add(item, items);
        }
    }
    public List<Item> GetPairIngredient(Item ingredientItem)
    {
        return pairIngredientDict[ingredientItem];

    }
    public Item GetResultItem(Item ingredientA, Item ingredientB)
    {
        return resultItemDict[(ingredientA, ingredientB)];
    }
}
