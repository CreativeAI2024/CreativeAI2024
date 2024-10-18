using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Purchasing.MiniJSON;
public enum WindowTypes
{
    MenuHome,
    Item,
    Action,
    ItemImage,
    Conversation,
    Combine
}

public class WindowManager : MonoBehaviour
{
    private WindowTypes currentWindow;
    
    public void SetCurrentWindow(WindowTypes currentWindow)
    {
        this.currentWindow = currentWindow;
    }
    public void ChangeWindowActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
}
