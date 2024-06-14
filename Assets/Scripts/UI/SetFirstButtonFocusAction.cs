using UnityEngine;
using UnityEngine.EventSystems;

public class SetButtonFocusAction : MonoBehaviour
{
    void OnEnable()
    {
        SetButtonFocus();
    }

    private void SetButtonFocus()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
