using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayDescripsion : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private ItemList itemList;
    private string focusedButtonName;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        SetFocusedButtonName();
        SetDescripsion();
    }
    void Update()
    {
        if (_inputSetting.GetForwardKeyDown() || _inputSetting.GetBackKeyDown() || _inputSetting.GetLeftKeyDown() || _inputSetting.GetRightKeyDown())
        {
            SetFocusedButtonName();
            SetDescripsion();
        }
    }
    private void SetDescripsion()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemList.Search(focusedButtonName).Description;
    }

    private void SetFocusedButtonName()
    {
        focusedButtonName = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }
}
