using UnityEngine;
using UnityEngine.EventSystems;

public class SetFirstFocus : MonoBehaviour
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
