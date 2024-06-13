using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    private Button button;
    private GameObject currentWindow;
    [SerializeField] private GameObject nextWindow;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        button = GetComponent<Button>(); //もっと賢い方法ある
        currentWindow = transform.parent.parent.gameObject;
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (_inputSetting.GetForwardKeyDown())
            {
                button.FindSelectableOnUp().Select();
            }
            if (_inputSetting.GetBackKeyDown())
            {
                button.FindSelectableOnDown().Select();
            }
            if (_inputSetting.GetLeftKeyDown())
            {
                button.FindSelectableOnLeft().Select();
            }
            if (_inputSetting.GetRightKeyDown())
            {
                button.FindSelectableOnRight().Select();
            }
            if (_inputSetting.GetDecideKeyDown())
            {
                Open();
            }
            if (_inputSetting.GetCancelKeyDown())
            {
                currentWindow.GetComponent<Window>().Cancel();
            }
        }
    }
    private void Open()
    {
        ChangeActive(nextWindow, true);
        ChangeActive(currentWindow, false);
    }
    private void ChangeActive(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
