using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemImageWindow : Window
{
    [SerializeField] private ItemActionWindow itemActionWindow;
    public override void OnDecide(Window previousWindow = null)
    {
        base.OnDecide(previousWindow);
        itemActionWindow.OnCancel();
    }
    public override void OnCancel()
    {
        base.OnCancel();
        itemActionWindow.OnCancel();
    }
}
