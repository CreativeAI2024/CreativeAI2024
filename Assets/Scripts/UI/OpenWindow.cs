using UnityEngine;
using UnityEngine.EventSystems;

public class OpenWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject currentWindow;
    [SerializeField] private GameObject nextWindow;
    void Start()
    {
        _inputSetting = InputSetting.Load();
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
    private void ChangeActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
}