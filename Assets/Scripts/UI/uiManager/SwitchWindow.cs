using UnityEngine;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject menuUI;
    // [SerializeField] private Pause pause; //何アタッチするのかわからない
    private bool _isWindowActive;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        _isWindowActive = menuUI.activeInHierarchy;
    }
    void Update()
    {
        if (_inputSetting.GetMenuKeyDown())
        {
            SetWindowActive(!_isWindowActive);
        }
        else if (_inputSetting.GetCancelKeyDown() && _isWindowActive && menuUI.activeSelf)
        {
            SetWindowActive(false);
        }
    }
    private void SetWindowActive(bool isActive)
    {
        menuUI.SetActive(isActive);
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
