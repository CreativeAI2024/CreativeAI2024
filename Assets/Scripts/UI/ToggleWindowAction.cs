using UnityEngine;

public class ToggleWindowAction : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject windowBox;
    [SerializeField] private GameObject topWindow;
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetItemKeyDown() || (_inputSetting.GetCancelKeyDown()&&topWindow.activeSelf))
        {
            ToggleWindow();
        }
    }
    public void ToggleWindow()
    {
        ChangeActive(windowBox, !windowBox.activeSelf);
    }
    private void ChangeActive(GameObject gameObject, bool isActive)
    {
        Debug.Log(gameObject);
        gameObject.SetActive(isActive);
    }
}
