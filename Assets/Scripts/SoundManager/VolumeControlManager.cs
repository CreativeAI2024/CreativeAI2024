using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VolumeControlManager : MonoBehaviour
{
    [SerializeField] private GameObject volumeControlObject;
    private Pause playerPause;
    [SerializeField] private Pause menuUIPause;
    [SerializeField] private Pause pause;

    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        playerPause ??= FindPause();
        ConversationTextManager.Instance.OnConversationStart += pause.PauseAll;
        ConversationTextManager.Instance.OnConversationEnd += pause.UnPauseAll;
    }

    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        playerPause = FindPause();
    }

    Pause FindPause()
    {
        return GameObject.Find("Pause").GetComponent<Pause>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleVolumeControl();
        }
    }

    void ToggleVolumeControl()
    {
        if (volumeControlObject.activeSelf)
        {
            playerPause.UnPauseAll();
            menuUIPause.UnPauseAll();
            DebugLogger.Log("パネルを非表示にします");
        }
        else
        {
            playerPause.PauseAll();
            menuUIPause.PauseAll();
            DebugLogger.Log("パネルを表示します");
        }
        volumeControlObject.SetActive(!volumeControlObject.activeSelf);
    }
}
