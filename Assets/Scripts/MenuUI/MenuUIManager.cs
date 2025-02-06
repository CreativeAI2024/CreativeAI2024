using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : DontDestroySingleton<MenuUIManager>
{
    [SerializeField] private Pause menuUIPause;
    private void Start()
    {
        ConversationTextManager.Instance.OnConversationStart += menuUIPause.PauseAll;
        ConversationTextManager.Instance.OnConversationEnd += menuUIPause.UnPauseAll;
    }
    
    public bool IsMenuUIActive()
    {
        return SceneManager.GetActiveScene().name.Contains("room");
    }
}
