using UnityEngine;
using UnityEngine.EventSystems;

public class SetButtonFocus : MonoBehaviour
{
    void OnEnable()
    {
        SetFocus();
    }

    private void SetFocus()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
