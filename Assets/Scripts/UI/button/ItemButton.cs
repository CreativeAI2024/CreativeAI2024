using System;
using TMPro;
using UnityEngine;

public class ItemButton : MonoBehaviour, IFocusObject
{
    [SerializeField] private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemDescription;
    private Item item;
    private ItemActionWindow itemActionWindow;
    private Action onDecide;
    private Action onCancel;
    
    public void Initialize(Item item, ItemActionWindow itemActionWindow, Action onDecide, Action onCancel, TextMeshProUGUI itemDescription)
    {
        itemName.text = item.ItemName;
        this.item = item;
        this.itemActionWindow = itemActionWindow;
        this.onCancel = onCancel;
        this.onDecide = onDecide;
        this.itemDescription = itemDescription;
    }
    
    public void OnDecideKeyDown()
    {
        itemActionWindow.Initialize(item);
        onDecide?.Invoke();
    }
    
    public void OnCancelKeyDown()
    {
        onCancel?.Invoke();
    }
}
