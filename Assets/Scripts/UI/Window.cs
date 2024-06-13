using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] public GameObject previousWindow;
    [SerializeField] private UIManager uiManager;

    public void Cancel()
    {
        if (previousWindow != null)
        {
            ChangeActive(gameObject, false);
            ChangeActive(previousWindow, true);
        }
        else
        {
            uiManager.ToggleWindow();
        }
    }
    private void ChangeActive(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
