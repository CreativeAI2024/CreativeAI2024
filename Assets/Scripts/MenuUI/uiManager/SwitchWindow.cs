using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private FollowFocusedButton followFocusedButton;
    [SerializeField] private Pause volumeControllerPause;
    private Pause playerPause;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        DebugLogger.Log("OnConversationStart/End called.");
        SceneManager.sceneLoaded += SceneLoaded;
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
                playerPause.UnPauseAll();
            }
            else
            {
                SetWindowActive(true);
                volumeControllerPause.PauseAll();
                playerPause.PauseAll();
            }
        }
        else if (_inputSetting.GetCancelKeyDown() && menuUI.activeInHierarchy)
        {
            SetWindowActive(false);
            playerPause.UnPauseAll();
        }
    }
    
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        if (!nextScene.name.Contains("room")) return;
        
        playerPause = GameObject.Find("Pause").GetComponent<Pause>();
    }

    private void SetWindowActive(bool isActive)
    {
        SoundManager.Instance.PlaySE(13, 1f);
        menuUI.SetActive(isActive);
        followFocusedButton.ScrollToTop();
    }
}
