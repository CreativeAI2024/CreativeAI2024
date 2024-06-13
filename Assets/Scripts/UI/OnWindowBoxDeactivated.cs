using UnityEngine;
using UnityEngine.EventSystems;

public class OnWindowBoxDeactivated : MonoBehaviour
{
    public delegate void ResetWindow();
    private ResetWindow ResetWindowCallback;
    void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        ResetWindowCallback();
    }

    public void Init(ResetWindow resetWindowActive)
    {
        ResetWindowCallback = resetWindowActive;
    }
}
