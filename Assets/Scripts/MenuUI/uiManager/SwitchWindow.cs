using UnityEngine;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject menuUI;

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
        SoundManager.Instance.PlaySE(0, 5f);
        menuUI.SetActive(isActive);
    }
}
