using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButtonFocus : MonoBehaviour
{
    private InputSetting _inputSetting;
    private Button _button;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        _button = GetComponent<Button>();
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

    private bool isSelected()
    {
        return (EventSystem.current.currentSelectedGameObject == gameObject);
    }
    private void FocusUpButton()
    {
        if (isSelected())
        _button.FindSelectableOnUp().Select();
    }
    private void FocusDownButton()
    {
        if (isSelected())
        _button.FindSelectableOnDown().Select();
    }
    private void FocusLeftButton()
    {
        if (isSelected())
        _button.FindSelectableOnLeft().Select();
    }
    private void FocusRightButton()
    {
        if (isSelected())
        _button.FindSelectableOnRight().Select();
    }
}