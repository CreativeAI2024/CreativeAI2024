using UnityEngine;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject windowBox;
    [SerializeField] private GameObject topWindow;
    [SerializeField] private Pause pause;
    private bool _isWindowActive = false;
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetMenuKeyDown())
        {
            SetWindowActive(!_isWindowActive);
        }
        else if (_inputSetting.GetCancelKeyDown() && _isWindowActive && topWindow.activeSelf)
        {
            SetWindowActive(false);
        }
    }
    private void SetWindowActive(bool isActive)
    {
        windowBox.SetActive(isActive);
        _isWindowActive = isActive;
        if (isActive)
        {
            pause.PauseAll();
        }
        else
        {
            pause.UnPauseAll(); 
        }
    }
}
