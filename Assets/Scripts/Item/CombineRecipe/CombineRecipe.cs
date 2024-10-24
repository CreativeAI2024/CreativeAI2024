using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CombineRecipe", menuName = "ScriptableObject/Item/CombineRecipe")]
public class CombineRecipe : ScriptableObject
{
    [SerializeField] private Item ingredientA;
    [SerializeField] private Item ingredientB;
    [SerializeField] private Item resultItem;
    public Item IngredientA => ingredientA;
    public Item IngredientB => ingredientB;
    public Item ResultItem => resultItem;
}
