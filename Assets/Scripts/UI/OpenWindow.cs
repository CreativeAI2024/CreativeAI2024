using UnityEngine;
using UnityEngine.EventSystems;

public class OpenWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject parentWindow;
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
        ChangeActive(parentWindow, false);
    }
    private void ChangeActive(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}