using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemWindow : MonoBehaviour
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private ItemButton itemButtonPrefab;
    [SerializeField] private Transform itemButtonGroup;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private ItemImageWindow itemImageWindow;
    [SerializeField] private DescriptionWindow descriptionWindow;
    private readonly Dictionary<Item, GameObject> itemButtonDict = new();
    void OnEnable()
    {
        DebugLogger.Log("itemImageWindow: "+itemImageWindow.gameObject.activeSelf);
        DebugLogger.Log("descriptionWindow: "+descriptionWindow.gameObject.activeSelf);
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
            DebugLogger.Log("item: "+item.ItemName);
            if (!itemButtonDict.ContainsKey(item))
            {
                GameObject itemButton = MakeItemButton(item);
                itemButtonDict.Add(item, itemButton);
            }
        }
        EventSystem.current.SetSelectedGameObject(itemButtonGroup.GetChild(0).gameObject);
    }
    
    private GameObject MakeItemButton(Item item)
    {
        ItemButton itemButton = Instantiate(itemButtonPrefab, itemButtonGroup);
        itemButton.Initialize(item, menuUI, itemImageWindow, descriptionWindow);
        return itemButton.gameObject;
    }
}