using System;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField, Multiline] private string discription;

    public string GetItemName() => itemName;
    public string GetDiscription() => discription;
}
