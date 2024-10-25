using System;
using TMPro;
using UnityEngine;

public abstract class ItemActionButton : MonoBehaviour, IFocusObject
{
    protected Item item;
    private Action onCancel;
    protected Window window; 
    
    public virtual void Initialize(Item item, Action onCancel, Window window)
    {
        this.item = item;
        this.onCancel = onCancel;
        this.window = window;
    }
    
    public abstract void OnDecideKeyDown();
    
    public void OnCancelKeyDown()
    {
        onCancel?.Invoke();
    }
}