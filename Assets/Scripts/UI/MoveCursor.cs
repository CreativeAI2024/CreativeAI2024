using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCursor : MonoBehaviour
{
    private InputSetting _inputSetting;
    private Selectable focusedButton;
    [SerializeField] private Transform cursorTransform;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        focusedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
    }

    void Update()
    {
        if (_inputSetting.GetForwardKeyDown())
        {
            FocusUpButton();
            SetPosition();
        }
        if (_inputSetting.GetBackKeyDown())
        {
            FocusDownButton();
            SetPosition();
        }
        if (_inputSetting.GetLeftKeyDown())
        {
            FocusLeftButton();
            SetPosition();
        }
        if (_inputSetting.GetRightKeyDown())
        {
            FocusRightButton();
            SetPosition();
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
    private void FocusUpButton()
    {
        Selectable focusCandidate = focusedButton.FindSelectableOnUp();
        FocusButton(focusCandidate);
    }
    private void FocusDownButton()
    {
        Selectable focusCandidate = focusedButton.FindSelectableOnDown();
        FocusButton(focusCandidate);
    }    private void FocusLeftButton()
    {
        Selectable focusCandidate = focusedButton.FindSelectableOnLeft();
        FocusButton(focusCandidate);
    }    private void FocusRightButton()
    {
        Selectable focusCandidate = focusedButton.FindSelectableOnRight();
        FocusButton(focusCandidate);
    }
    private void SetPosition()
    {
        cursorTransform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
}