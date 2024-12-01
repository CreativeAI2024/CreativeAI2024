using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : DontDestroySingleton<MenuUIManager>
{
    [SerializeField] private Pause menuUIPause;
    private Pause playerPause;
    public Pause PlayerPause => playerPause;
    private bool isMenuUIActive = false;
    void Start()
    {
        ConversationTextManager.Instance.OnConversationStart += menuUIPause.PauseAll;
        ConversationTextManager.Instance.OnConversationEnd += menuUIPause.UnPauseAll;
        SceneManager.sceneLoaded += SceneLoaded;
    }
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        isMenuUIActive = nextScene.name.Contains("room");
        playerPause = GameObject.Find("Pause").GetComponent<Pause>();
    }
    public bool GetIsMenuUIActive()
    {
        return isMenuUIActive;
    }
}
