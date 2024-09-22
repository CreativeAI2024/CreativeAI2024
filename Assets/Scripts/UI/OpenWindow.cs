using UnityEngine;
using UnityEngine.EventSystems;

public class OpenWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    public GameObject currentWindow;
    public GameObject nextWindow;
    void Start()
    {
        // Debug.Log("OpenWindow on "+gameObject);
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            // Debug.Log("OpenWindow-------------------");
            // Debug.Log("Z pressed.");
            // Debug.Log("EventSystem.current.currentSelectedGameObject: "+EventSystem.current.currentSelectedGameObject);
            // Debug.Log("gameObject: "+gameObject);
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                Open();
            }
        }
    }
    void Open()
    {
        // Debug.Log("Open() called");
        ChangeActive(nextWindow, true);
        ChangeActive(currentWindow, false);
    }
    private void ChangeActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
}