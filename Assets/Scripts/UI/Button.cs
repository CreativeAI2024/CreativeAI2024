using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private InputSetting _inputSetting;
    private Button button;
    private GameObject currentWindow;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        button = GetComponent<Button>();
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
                button.onClick.Invoke();
            }
            if (_inputSetting.GetCancelKeyDown())
            {
                currentWindow.GetComponent<Window>().Cancel();
            }
        }
    }
}
