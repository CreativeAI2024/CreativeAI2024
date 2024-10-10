using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayDescription : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private ItemInventory itemInventory;
    private BaseItem focusedButton;
    private bool isOnEnableFirstRun = true;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        SetFocusedButton();
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
            SetFocusedButton();
            SetDescription();
        }
    }
    void Update()
    {
        if (_inputSetting.GetForwardKeyDown() || _inputSetting.GetBackKeyDown() || _inputSetting.GetLeftKeyDown() || _inputSetting.GetRightKeyDown())
        {
            SetFocusedButton();
            SetDescription();
        }
    }
    private void SetDescription()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = focusedButton.Description;//フォーカスしてるボタンから取得した名前からアイテムを取得してる
    }

    private void SetFocusedButton()
    {
        focusedButton = itemInventory.GetItem(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
    }
}
