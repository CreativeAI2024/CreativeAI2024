using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CombineRecipeDatabase", menuName = "ScriptableObject/Item/CombineRecipeDatabase")]
public class CombineRecipeDatabase : ScriptableObject
{
    [SerializeField] private List<CombineRecipe> recipes;
    private Dictionary<Item, Dictionary<Item, Item>> recipeDict;
    public void Initialize()
    {
        recipeDict = new ();
        foreach (CombineRecipe recipe in recipes)
        {
            SetPairIngredient(recipe.IngredientA, recipe.IngredientB, recipe.ResultItem);
            SetPairIngredient(recipe.IngredientB, recipe.IngredientA, recipe.ResultItem);
        }
    }

    private void SetPairIngredient(Item item, Item pair, Item result)
    {
        if (!recipeDict.ContainsKey(item))
        {
            Dictionary<Item, Item> items = new(){
                {pair, result}
            };
            recipeDict.Add(item, items);
        }else{
            recipeDict[item].Add(pair, result);
        }
    }
    public HashSet<Item> GetPairIngredients(Item ingredientItem)
    {
        return recipeDict[ingredientItem].Keys.ToHashSet();

    }
    public Item GetResultItem(Item ingredientA, Item ingredientB)
    {
        return recipeDict[ingredientA][ingredientB];
    }
}