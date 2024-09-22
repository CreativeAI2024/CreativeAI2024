using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCursor : MonoBehaviour
{
    private Selectable focusedButton;
    void Start()
    {
        Debug.Log("MoveCursor on "+transform.parent.gameObject);
        focusedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        Debug.Log("focusesButton: "+focusedButton);
        ChangeCursorVisibility(focusedButton.gameObject, true);
    }

    void Update()
    {
        // Debug.Log("focusedButton: "+focusedButton);
        // Debug.Log("EventSystem.current.currentSelectedGameObject: "+EventSystem.current.currentSelectedGameObject);
        if (focusedButton != EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>())
        {
            Debug.Log("focus changed");//←この行に入ってない
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
        Debug.Log("FocusButton succeeded");
        }
    }
    private void ChangeCursorVisibility(GameObject button, bool isEnabled)
    {
        Debug.Log("ChangeCursorVisibility("+button+", "+isEnabled+") called");
        button.GetComponent<Image>().enabled = isEnabled;
    }
}