using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    public GameObject currentWindow;
    public GameObject nextWindow;
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            //OpenWindowが有効化できていない
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                Open();
            }
        }
    }
    void Open()
    {
        ChangeActive(nextWindow, true);
        ChangeActive(currentWindow, false);
    }
    private void ChangeActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
}