using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private InputSetting _inputSetting;
    private Button button;
    private GameObject currentWindow;
    private GameObject previousWindow;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        button = GetComponent<Button>();
        currentWindow = transform.parent.parent.gameObject;
        previousWindow = currentWindow.GetComponent<Window>().previousWindow;
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
                changeActive(currentWindow, false);
                changeActive(previousWindow, true);
            }
        }
    }
    private void changeActive(GameObject gameObject, bool isVisible)
    {
        if (gameObject == null)
        {
            Debug.Log("gameObject of changeActive() is null.");
        }
        else
        {
            gameObject.SetActive(isVisible);
        }
    }
}
