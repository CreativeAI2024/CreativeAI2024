using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombineIngredientsWindow : Window
{
    [SerializeField] private ItemInventory itemInventory;
    private Item baseItem;
    [SerializeField] private CombineWindow combineWindow;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
    [SerializeField] private CombinePairItemButton combinePairItemButtonPrefab;
    [SerializeField] private DiscombinableButton discombinableButtonPrefab;
    [SerializeField] private Transform ingredientButtonGroup;
    private readonly List<GameObject> ingredientButtons = new();
    public void Initialize(Item baseItem)
    {
        this.baseItem = baseItem;
        LoadItemInventory();
    }
    private void LoadItemInventory()
    {
        foreach (GameObject button in ingredientButtons)
        {
            Destroy(button);
            
        }

        bool isFirst = true;
        foreach (Item item in itemInventory.GetItems())
        {
            GameObject itemButton = MakeButton(item);
            if (isFirst)
            {
                isFirst = false;
                EventSystem.current.SetSelectedGameObject(itemButton);
            }
            ingredientButtons.Add(itemButton);
        }
    }
    private GameObject MakeButton(Item item)
    {
        if (item.ItemName.Equals(baseItem.ItemName)) return null;
        if (itemInventory.HasAnyPairIngredients(item))
        {
            CombinePairItemButton combinePairItemButton = Instantiate(combinePairItemButtonPrefab, ingredientButtonGroup);
            combinePairItemButton.Initialize(combineWindow, item, baseItem, OnDecide, base.OnCancel);
            return combinePairItemButton.gameObject;
        }
        else
        {
            DiscombinableButton discombinableButton = Instantiate(discombinableButtonPrefab, ingredientButtonGroup);
            discombinableButton.Initialize(item);
            return discombinableButton.gameObject;
        }
    }
    private void OnDecide()
    {
        base.OnDecide();
    }
}