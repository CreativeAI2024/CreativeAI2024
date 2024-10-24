using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowButton : MonoBehaviour, IFocusObject
{
    [SerializeField] private Window window;
    
    [SerializeField] private Window nextWindow;
    
    public void OnDecideKeyDown()
    {
        nextWindow.OnDecideKeyDown(window);
    }
    
    public void OnCancelKeyDown()
    {
        window.OnCancelKeyDown();
    }
}
