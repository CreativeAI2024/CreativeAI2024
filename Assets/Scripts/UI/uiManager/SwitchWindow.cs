using UnityEngine;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject windowBox;
    [SerializeField] private GameObject topWindow;
    // [SerializeField] private Pause pause; //何アタッチするのかわからない
    private bool _isWindowActive;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        _isWindowActive = windowBox.activeInHierarchy;
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
            // pause.PauseAll();
        }
        else
        {
            // pause.UnPauseAll(); 
        }
    }
}
