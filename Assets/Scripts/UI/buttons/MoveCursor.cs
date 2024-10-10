using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCursor : MonoBehaviour
{
    private bool isOnEnableFirstRun = true;
    protected Selectable focusedButton;
    void OnEnable()

    {
        if (isOnEnableFirstRun)
        {
            isOnEnableFirstRun = false;
        }
        else
        {
            Setup();
        }
    }

    void Start()
    {
        Setup();
    }

    void Update()
    {
        if (focusedButton.gameObject != EventSystem.current.currentSelectedGameObject)
        {
            ChangeCursorVisibility(focusedButton.gameObject, false);
            FocusButton(EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>());
            ChangeCursorVisibility(focusedButton.gameObject, true);
        }
    }

    private void Setup()
    {
        if (isOnEnableFirstRun)
        {
            isOnEnableFirstRun = false;
        }
        else
        {
            Debug.Log("EventSystem.current.currentSelectedGameObject: "+EventSystem.current.currentSelectedGameObject);
            focusedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
            Debug.Log("focusedButton: "+focusedButton.gameObject);
            foreach (Transform child in transform)
            {
                ChangeCursorVisibility(child.gameObject, false);
            }
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