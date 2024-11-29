using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : DontDestroySingleton<MenuUIManager>
{
    [SerializeField] private Pause pause;
    private bool isMenuUIActive = false;
    void Start()
    {
        ConversationTextManager.Instance.OnConversationStart += pause.PauseAll;
        ConversationTextManager.Instance.OnConversationEnd += pause.UnPauseAll;
        SceneManager.sceneLoaded += SceneLoaded;
    }
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        isMenuUIActive = nextScene.name.Contains("room");
    }
    public bool GetIsMenuUIActive()
    {
        return isMenuUIActive;
    }
}
