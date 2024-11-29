using UnityEngine;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private FollowFocusedButton followFocusedButton;

    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (MenuUIManager.Instance.GetIsMenuUIActive())
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
    }
    private void SetWindowActive(bool isActive)
    {
        menuUI.SetActive(isActive);
        followFocusedButton.ScrollToTop();
    }
}
