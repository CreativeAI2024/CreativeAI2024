using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="BaseItem", menuName ="ScriptableObject/Item/BaseItem")]
public class BaseItem : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField, TextArea] private string description;
    [SerializeField] private int count = 1;
    private bool isCombinable = false;
    public string ItemName => itemName;
    public string Description => description;
    public int Count => count;
    public bool IsCombinable { get; set; }

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
