using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ItemWindow : Window
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
    [SerializeField] private ItemButton itemButtonPrefab;
    [SerializeField] private Transform itemWindowButtons;
    [SerializeField] private TextMeshProUGUI itemDescriptionPanel;
    private readonly Dictionary<Item, GameObject> itemButtonDict = new();
    void OnEnable()
    {
        LoadItemInventory();
    }
    private void LoadItemInventory()
    {
        foreach (KeyValuePair<Item, GameObject> item in itemButtonDict)
        {
            if (!itemInventory.GetItems().Contains(item.Key))
            {
                Destroy(item.Value);
            }
        }
        foreach (Item item in itemInventory.GetItems())
        {
            if (!itemButtonDict.ContainsKey(item))
            {
                GameObject itemButton = MakeItemButton(item);
                itemButtonDict.Add(item, itemButton);
            }
        }
    }
    private GameObject MakeItemButton(Item item)
    {
        ItemButton itemButton = Instantiate(itemButtonPrefab, itemWindowButtons);
        itemButton.Initialize(item, itemDescriptionPanel);
        return itemButton.gameObject;
    }

}