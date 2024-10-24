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
    [SerializeField] private CombineIngredientsWindow combineIngredientsWindow;
    public override void Initialize(Item _item, string buttonText, Action onCancel, Window window)
    {
        base.Initialize(_item, buttonText, onCancel, window);
    }    
    public override void OnDecideKeyDown()
    {
        combineIngredientsWindow.Initialize(item);
    }
}