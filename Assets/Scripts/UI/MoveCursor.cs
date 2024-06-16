using System;
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
        FocusButton(focusedButton.FindSelectableOnUp());
    }

    private void FocusDownButton()
    {
        FocusButton(focusedButton.FindSelectableOnDown());
    }
    private void FocusLeftButton()
    {
        FocusButton(focusedButton.FindSelectableOnLeft());
    }
    private void FocusRightButton()
    {
        FocusButton(focusedButton.FindSelectableOnRight());
    }
    private void SetPosition()
    {
        cursorTransform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
}