using UnityEngine;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private FollowFocusedButton followFocusedButton;

    [SerializeField] Pause volumePause;

    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (!MenuUIManager.Instance.IsMenuUIActive()) return;
        if (_inputSetting.GetMenuKeyDown())
        {
            if (menuUI.activeInHierarchy)
            {
                SetWindowActive(false);
                MenuUIManager.Instance.PlayerPause.UnPauseAll();
                volumePause.UnPauseAll();
            }
            else
            {
                SetWindowActive(true);
                MenuUIManager.Instance.PlayerPause.PauseAll();
                volumePause.UnPauseAll();
            }
        }
        else if (_inputSetting.GetCancelKeyDown() && menuUI.activeInHierarchy)
        {
            SetWindowActive(false);
            MenuUIManager.Instance.PlayerPause.UnPauseAll();
        }
    }


    private void SetWindowActive(bool isActive)
    {
        SoundManager.Instance.PlaySE(16, 4f);
        menuUI.SetActive(isActive);
        followFocusedButton.ScrollToTop();
    }
}
