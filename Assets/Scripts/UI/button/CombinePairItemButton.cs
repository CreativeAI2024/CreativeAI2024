using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombinePairItemButton : MonoBehaviour, IPushedObject
{
    [SerializeField] private TextMeshProUGUI itemName;
    private CombineWindow combineWindow;
    private Item pairItem;
    private Item baseItem;
    private Action onDecide;
    private Action onCancel;
    
    public void Initialize(CombineWindow combineWindow, Item pairItem, Item baseItem, Action onDecide, Action onCancel)
    {
        itemName.text = pairItem.ItemName;
        this.combineWindow = combineWindow;
        this.pairItem = pairItem;
        this.baseItem = baseItem;
        this.onCancel = onCancel;
        this.onDecide = onDecide;
    }
    public void OnDecideKeyDown()
    {
        combineWindow.Initialize(baseItem, pairItem);
        onDecide?.Invoke();
    }
    
    public void OnCancelKeyDown()
    {
        onCancel?.Invoke();
    }
}
