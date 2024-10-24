using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombineButton : ItemActionButton
{
    [SerializeField] private Selectable selectable;
    CombineItems combineItems;
    public override void Initialize(Item _item, string buttonText, Action onCancel, Window window)
    {
        base.Initialize(_item, buttonText, onCancel, window);
        combineItems = new CombineItems(_item);
        SetEnabled(combineItems.HasPairItemInInventory(_item));
    }

    private void SetEnabled(bool isEnabled)
    {
        selectable.enabled = isEnabled;
        buttonText.color = isEnabled ? Color.white : Color.grey;
    }
    
    public override void OnDecideKeyDown()
    {
        combineItems.Combine(null); //
    }
}