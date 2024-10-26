using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemButton : MonoBehaviour, IPushedObject, IFocusedObject
{
    [SerializeField] private TextMeshProUGUI itemName;
    private DescriptionWindow descriptionWindow;
    private Item item;
    private GameObject menuUI;
    
    public void Initialize(Item item, GameObject menuUI, DescriptionWindow descriptionWindow)
    {
        itemName.text = item.ItemName;
        this.item = item;
        this.descriptionWindow = descriptionWindow;
        this.menuUI = menuUI;
    }
    
    public void OnFocused()
    {
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
