using UnityEngine;
using UnityEngine.EventSystems;

public class GridLayoutGroup : MonoBehaviour
{
    [SerializeField] GameObject firstButton;
    void OnEnable()
    {
        FocusHeadButton();
    }

    private void FocusHeadButton()
    {
        if(!IsCurrentEventSystemNull() && firstButton!=null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }
    private bool IsCurrentEventSystemNull()
    {
        return EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null;
    }
}
