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
        if(!UIUtility.IsCurrentEventSystemNull() && firstButton!=null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }
}
