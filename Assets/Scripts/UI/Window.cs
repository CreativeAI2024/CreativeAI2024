using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] public GameObject previousWindow;
    [SerializeField] private GameObject uiManager;

    public void Cancel()
    {
        if (previousWindow != null)
        {
            UIUtility.ChangeActive(gameObject, false);
            UIUtility.ChangeActive(previousWindow, true);
        }
        else
        {
            uiManager.GetComponent<UIManager>().ToggleWindow();
        }
    }
}
