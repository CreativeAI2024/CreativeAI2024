using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="Item", menuName ="ScriptableObject/Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField, TextArea] private string description;
    [SerializeField] private int count = 1;
    [SerializeField] private Sprite image;
    [SerializeField] private List<string> text;
    public string ItemName => itemName;
    public string Description => description;
    public int Count => count;
    public Sprite Image => image;
    public List<string> Text => text;


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
