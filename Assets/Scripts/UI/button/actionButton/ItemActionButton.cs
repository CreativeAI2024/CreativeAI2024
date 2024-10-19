using System;
using TMPro;
using UnityEngine;

public abstract class ItemActionButton : MonoBehaviour, IFocusObject
{
    [SerializeField] protected TextMeshProUGUI buttonText;
    protected Item item;
    private Action onCancel;
    protected Window window; 
    
    public virtual void Initialize(Item item, string buttonText, Action onCancel, Window window)
    {
        this.item = item;
        this.buttonText.text = buttonText;
        this.onCancel = onCancel;
        this.window = window;
    }
    
    public abstract void OnDecideKeyDown();
    
    public void OnCancelKeyDown()
    {
        onCancel?.Invoke();
    }
}