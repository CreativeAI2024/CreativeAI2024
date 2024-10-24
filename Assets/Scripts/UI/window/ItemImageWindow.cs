using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemImageWindow : Window
{
    public void Open(Window window)
    {
        base.OnDecideKeyDown(window);
    }
}
