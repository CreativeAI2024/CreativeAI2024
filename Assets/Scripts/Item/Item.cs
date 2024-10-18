using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="Item", menuName ="ScriptableObject/Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField, TextArea] private string descriptionText;
    [SerializeField] private int count = 1;
    [SerializeField] private Sprite image;
    [SerializeField] private List<string> contentText;
    public string ItemName => itemName;
    public string DescriptionText => descriptionText;
    public int Count => count;
    public Sprite Image => image;
    public List<string> ContentText => contentText;


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
