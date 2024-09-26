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
            // Debug.Log("EventSystem.current.currentSelectedGameObject: "+EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            // Debug.Log("OpenWindow on : "+gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
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