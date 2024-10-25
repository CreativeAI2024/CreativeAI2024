using UnityEngine;

public class WindowButton : MonoBehaviour, IFocusObject
{
    [SerializeField] private Window window;
    
    [SerializeField] private Window nextWindow;
    
    public void OnDecideKeyDown()
    {
        nextWindow.OnDecide(window);
    }
    
    public void OnCancelKeyDown()
    {
        window.OnCancel();
    }
}
