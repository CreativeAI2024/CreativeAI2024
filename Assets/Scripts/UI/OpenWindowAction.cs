using UnityEngine;
using UnityEngine.EventSystems;

public class OpenWindowAction : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject currentWindow;
    [SerializeField] private GameObject nextWindow;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        currentWindow = transform.parent.parent.gameObject;
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
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
    private void ChangeActive(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}