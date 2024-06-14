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
            FocusUpButton(focusedButton);
        }
        if (_inputSetting.GetBackKeyDown())
        {
            FocusDownButton(focusedButton);
        }
        if (_inputSetting.GetLeftKeyDown())
        {
            FocusLeftButton(focusedButton);
        }
        if (_inputSetting.GetRightKeyDown())
        {
            FocusRightButton(focusedButton);
        }
    }

    private bool isSelected()
    {
        return (EventSystem.current.currentSelectedGameObject == gameObject);
    }
    private void FocusUpButton(Selectable _button)
    {
        if (isSelected())
        _button.FindSelectableOnUp().Select();
    }
    private void FocusDownButton(Selectable _button)
    {
        if (isSelected())
        _button.FindSelectableOnDown().Select();
    }
    private void FocusLeftButton(Selectable _button)
    {
        if (isSelected())
        _button.FindSelectableOnLeft().Select();
    }
    private void FocusRightButton(Selectable _button)
    {
        if (isSelected())
        _button.FindSelectableOnRight().Select();
    }
}