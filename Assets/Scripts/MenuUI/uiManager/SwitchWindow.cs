using UnityEngine;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject main;
    [SerializeField] private FollowFocusedButton followFocusedButton;

    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetMenuKeyDown())
        {
            SetWindowActive(!main.activeInHierarchy);
        }
        else if (_inputSetting.GetCancelKeyDown() && main.activeInHierarchy)
        {
            SetWindowActive(false);
        }
    }
    private void SetWindowActive(bool isActive)
    {
        main.SetActive(isActive);
        followFocusedButton.ScrollToTop();
    }
}
