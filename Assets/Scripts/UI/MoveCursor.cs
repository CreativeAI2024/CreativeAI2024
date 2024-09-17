using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCursor : MonoBehaviour
{
    private Selectable focusedButton;
    void Start()
    {
        focusedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        ChangeCursorVisibility(focusedButton.gameObject, true);
    }

    void Update()
    {
        if (focusedButton != EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>())
        {
            ChangeCursorVisibility(focusedButton.gameObject, false);
            FocusButton(EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>());
            ChangeCursorVisibility(focusedButton.gameObject, true);
        }
    }
    private void FocusButton(Selectable focusCandidate)
    {
        if (focusCandidate != null)
        {
            focusedButton = focusCandidate;
            focusedButton.Select();
        }
    }
    private void ChangeCursorVisibility(GameObject button, bool isEnabled)
    {
        button.GetComponent<Image>().enabled = isEnabled;
    }
}