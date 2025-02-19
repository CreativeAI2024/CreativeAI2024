using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : DontDestroySingleton<MenuUIManager>
{
    [SerializeField] private Pause menuUIPause;
    private Pause playerPause;
    public Pause PlayerPause => playerPause;
    void Start()
    {
        ConversationTextManager.Instance.OnConversationStart += menuUIPause.PauseAll;
        ConversationTextManager.Instance.OnConversationEnd += menuUIPause.UnPauseAll;
        SceneManager.sceneLoaded += SceneLoaded;
    }
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        if (!nextScene.name.Contains("room")) return;
        
        playerPause = GameObject.Find("Pause").GetComponent<Pause>();
    }
    public bool IsMenuUIActive()
    {
        return SceneManager.GetActiveScene().name.Contains("room");
    }
}
