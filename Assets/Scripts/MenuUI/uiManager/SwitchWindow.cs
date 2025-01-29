using UnityEngine;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private FollowFocusedButton followFocusedButton;
    [SerializeField] private Pause volumeControllerPause;

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
                volumeControllerPause.UnPauseAll();
                MenuUIManager.Instance.PlayerPause.UnPauseAll();
            }
            else
            {
                SetWindowActive(true);
                volumeControllerPause.PauseAll();
                MenuUIManager.Instance.PlayerPause.PauseAll();
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
        SoundManager.Instance.PlaySE(13, 1f);
        menuUI.SetActive(isActive);
        followFocusedButton.ScrollToTop();
    }
}
