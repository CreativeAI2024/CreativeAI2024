using System;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    [SerializeField, TextArea] private string description;
    public string Description => description;
    private int count = 1;
    public int Count => count;

    public void IncrementCount()
    {
        count++;
    }
    
    public void DecrementCount()
    {
        if (count >= 1)
        {
            count--;
        }
        else
        {
            throw new InvalidOperationException("Count cannot be less than 1");
        }
    }
}
