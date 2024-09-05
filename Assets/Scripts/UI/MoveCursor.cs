using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCursor : MonoBehaviour
{
    private InputSetting _inputSetting;
    private Selectable focusedButton;
    [SerializeField] private GameObject imageOfImageShownItem;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        focusedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        ChangeCursorVisibility(focusedButton.gameObject, true);
    }

    void Update()
    {
        if (_inputSetting.GetForwardKeyDown() && !IsImageEnabled())
        {
            MoveToUpButton();
        }
        if (_inputSetting.GetBackKeyDown() && !IsImageEnabled())
        {
            MoveToDownButton();
        }
        if (_inputSetting.GetLeftKeyDown() && !IsImageEnabled())
        {
            MoveToLeftButton();
        }
        if (_inputSetting.GetRightKeyDown() && !IsImageEnabled())
        {
            MoveToRightButton();
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

    private void MoveToUpButton()
    {
        ChangeCursorVisibility(focusedButton.gameObject, false);
        FocusButton(focusedButton.FindSelectableOnUp());
        ChangeCursorVisibility(focusedButton.gameObject, true);
    }

    private void MoveToDownButton()
    {
        ChangeCursorVisibility(focusedButton.gameObject, false);
        FocusButton(focusedButton.FindSelectableOnDown());
        ChangeCursorVisibility(focusedButton.gameObject, true);
    }
    private void MoveToLeftButton()
    {
        ChangeCursorVisibility(focusedButton.gameObject, false);
        FocusButton(focusedButton.FindSelectableOnLeft());
        ChangeCursorVisibility(focusedButton.gameObject, true);
    }
    private void MoveToRightButton()
    {
        ChangeCursorVisibility(focusedButton.gameObject, false);
        FocusButton(focusedButton.FindSelectableOnRight());
        ChangeCursorVisibility(focusedButton.gameObject, true);
    }

    private bool IsImageEnabled()
    {
        return imageOfImageShownItem.GetComponent<Image>().enabled;
    }
}