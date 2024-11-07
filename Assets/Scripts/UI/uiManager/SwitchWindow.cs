using UnityEngine;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject menuUI;
    // [SerializeField] private Pause pause; //何アタッチするのかわからない

    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetMenuKeyDown())
        {
            SetWindowActive(!menuUI.activeInHierarchy);
        }
        else if (_inputSetting.GetCancelKeyDown() && menuUI.activeInHierarchy)
        {
            SetWindowActive(false);
        }
    }
    private void SetWindowActive(bool isActive)
    {
        menuUI.SetActive(isActive);
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
