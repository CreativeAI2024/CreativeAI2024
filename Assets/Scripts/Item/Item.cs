using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName ="Item", menuName ="ScriptableObject/Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField, TextArea] private string descriptionText;
    [SerializeField] private int count = 1;
    [FormerlySerializedAs("image")] [SerializeField] private Sprite sprite;
    [SerializeField] private List<string> contentText;
    public string ItemName => itemName;
    public string DescriptionText => descriptionText;
    public int Count => count;
    public Sprite Sprite => sprite;
    public List<string> ContentText => contentText;

    public bool HasContentText()
    {
        return ((contentText.Count!=0 || contentText[0].Length!=0));
    }
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
