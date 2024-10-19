using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemImageWindow : Window, IFocusObject
{
    public void Open(Window window)
    {
        base.OnDecideKeyDown(window);
    }
    
    public void OnDecideKeyDown()
    {
        // base.OnDecideKeyDown(window);
    }
}
