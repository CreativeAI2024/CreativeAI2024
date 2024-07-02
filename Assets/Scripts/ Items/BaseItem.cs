using System;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    [SerializeField] private String itemName;
    [SerializeField, Multiline] private String discription;

    public String GetItemName() => itemName;
    public String GetDiscription() => discription;
}
