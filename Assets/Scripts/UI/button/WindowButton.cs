using UnityEngine;

public class WindowButton : MonoBehaviour, IPushedObject
{
    [SerializeField] private Window window;
    
    [SerializeField] private Window nextWindow;
    //このボタンを押すと、次のウィンドウを開く。具体的にどう開くかは、移動先のウィンドウに記述する。
    public void OnDecideKeyDown()
    {
        nextWindow.OnDecide(window);
    }
    
    public void OnCancelKeyDown()
    {
        window.OnCancel();
    }
}
