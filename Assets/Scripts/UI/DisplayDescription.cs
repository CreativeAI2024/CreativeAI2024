using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//TODO: 改行機能作成
public class DisplayDescription : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private ItemList itemList;
    private string focusedButtonName;
    void Start()
    {
        // Debug.Log("DisplayDescription on "+gameObject);
        _inputSetting = InputSetting.Load();
        SetFocusedButtonName();
        SetDescription();
    }

    void OnEnable()
    {
        try
        {
            SetFocusedButtonName();
            SetDescription();
        }
        catch (Exception)
        {
            Debug.Log("First Run");
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
