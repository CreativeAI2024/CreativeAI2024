using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineWindow : Window
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
    [SerializeField] private Transform itemButtonGroup;

    public void Initialize(Item ingredientItemA, Item ingredientItemB)
    {

    }

    public void OnDecideKeyDown()
    {
        base.OnDecideKeyDown();
    }
}
