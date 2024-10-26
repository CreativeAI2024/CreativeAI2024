using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemButton : MonoBehaviour, IPushedObject, IFocusedObject
{
    [SerializeField] private TextMeshProUGUI itemName;
    private ItemImageWindow itemImageWindow;
    private DescriptionWindow descriptionWindow;
    private Item item;
    private GameObject menuUI;
    
    public void Initialize(Item item, GameObject menuUI, ItemImageWindow itemImageWindow, DescriptionWindow descriptionWindow)
    {
        itemName.text = item.ItemName;
        this.item = item;
        this.itemImageWindow = itemImageWindow;
        this.descriptionWindow = descriptionWindow;
        this.menuUI = menuUI;
    }
    
    public void OnFocused()
    {
        itemImageWindow.SetImage(item.Sprite);
        descriptionWindow.SetText(item.DescriptionText);
    }
    public void OnDecideKeyDown()
    {
        Debug.Log(item.ItemName+" pushed.");
    }
    
    public void OnCancelKeyDown()
    {
        menuUI.SetActive(false);
    }
}
