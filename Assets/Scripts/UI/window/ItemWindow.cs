using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ItemWindow : Window
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private ItemActionWindow itemActionWindow;
    [SerializeField] private ItemButton itemButtonPrefab;
    [SerializeField] private Transform itemButtonGroup;
    [SerializeField] private TextMeshProUGUI itemDescription;
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
        
        bool isFirst = true;
        foreach (Item item in itemInventory.GetItems())
        {
            if (!itemButtonDict.ContainsKey(item))
            {
                GameObject itemButton = MakeItemButton(item);
                if (isFirst)
                {
                    isFirst = false;
                    EventSystem.current.SetSelectedGameObject(itemButton);
                }
                itemButtonDict.Add(item, itemButton);
            }
        }
    }
    
    private GameObject MakeItemButton(Item item)
    {
        ItemButton itemButton = Instantiate(itemButtonPrefab, itemButtonGroup);
        itemButton.Initialize(item, itemActionWindow, OnDecide, base.OnCancel, itemDescription);
        return itemButton.gameObject;
    }
    
    private void OnDecide()
    {
        itemActionWindow.OnDecide();
    }
}