using System;
using TMPro;
using UnityEngine;

public class CombineExecuteButton : MonoBehaviour, IPushedObject
{
    [SerializeField] private ItemInventory itemInventory;
    private Item ingredientItemA;
    private Item ingredientItemB;
    private Item resultItem;
    private Action onDecide;
    private Action onCancel;
    
    public void Initialize(Item ingredientItemA, Item ingredientItemB, Item resultItem, Action onDecide, Action onCancel)
    {
        this.ingredientItemA = ingredientItemA;
        this.ingredientItemB = ingredientItemB;
        this.resultItem = resultItem;
        this.onCancel = onCancel;
        this.onDecide = onDecide;
    }
    
    public void OnDecideKeyDown()
    {
        itemInventory.Remove(ingredientItemA);
        itemInventory.Remove(ingredientItemB);
        itemInventory.Add(resultItem);
        onDecide?.Invoke();
    }
    
    public void OnCancelKeyDown()
    {
        onCancel?.Invoke();
    }
}
