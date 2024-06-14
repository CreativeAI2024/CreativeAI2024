using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButtonFocus : MonoBehaviour
{
    private InputSetting _inputSetting;
    private Selectable focusedButton;
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
        }
        if (_inputSetting.GetBackKeyDown())
        {
            FocusDownButton();
        }
        if (_inputSetting.GetLeftKeyDown())
        {
            FocusLeftButton();
        }
        if (_inputSetting.GetRightKeyDown())
        {
            FocusRightButton();
        }
    }
    private void FocusUpButton()
    {
        focusedButton = focusedButton.FindSelectableOnUp();
        focusedButton.Select();
    }
    private void FocusDownButton()
    {
        focusedButton = focusedButton.FindSelectableOnDown();
        focusedButton.Select();
    }
    private void FocusLeftButton()
    {
        focusedButton = focusedButton.FindSelectableOnLeft();
        focusedButton.Select();
    }
    private void FocusRightButton()
    {
        focusedButton = focusedButton.FindSelectableOnRight();
        focusedButton.Select();
    }
}