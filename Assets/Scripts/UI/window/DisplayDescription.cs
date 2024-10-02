using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayDescription : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private ItemList itemList;
    private string focusedButtonName;
    private bool isOnEnableFirstRun = true;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        SetFocusedButtonName();
        SetDescription();
    }

    void OnEnable()
    {
        if (isOnEnableFirstRun)
        {
            isOnEnableFirstRun = false;
        }
        else
        {
            SetFocusedButtonName();
            SetDescription();
        }
    }
    void Update()
    {
        if (_inputSetting.GetForwardKeyDown() || _inputSetting.GetBackKeyDown() || _inputSetting.GetLeftKeyDown() || _inputSetting.GetRightKeyDown())
        {
            SetFocusedButtonName();
            SetDescription();
        }
    }
    private void SetDescription()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemList.Search(focusedButtonName).Description;
    }

    private void SetFocusedButtonName()
    {
        focusedButtonName = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }
}
