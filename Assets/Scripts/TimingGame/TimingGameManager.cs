using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimingGameManager : MonoBehaviour
{
    [SerializeField] TimingGame timingGame;
    [SerializeField] GameObject timingGameObject;

    void Update()
    {
        if (timingGame.GetEndFlag())
        {
            EndTimingGame();
        }
        if ((FlagManager.Instance.HasFlag("StartTimingGame1") ^ FlagManager.Instance.HasFlag("StartTimingGame2")))
        {
            Initialize(); 
        }
    }

    public void Initialize()
    {
        timingGameObject.SetActive(true);
        timingGame.Initialize();
    }

    private void EndTimingGame()
    {
        TimingGameSuccessFlag();
        timingGameObject.SetActive(false);
        FlagManager.Instance.DeleteFlag("StartTimingGame1");
        FlagManager.Instance.DeleteFlag("StartTimingGame2");
        DebugLogger.Log($"{FlagManager.Instance.HasFlag("Progress9")}:{FlagManager.Instance.HasFlag("Progress8")}");
        SceneManager.LoadScene("reference_room");
    }

    private void TimingGameSuccessFlag()
    {
        if (!FlagManager.Instance.HasFlag("StartTimingGame2"))
            return;

        if (timingGame.GetSuccessFlag())
        {
            FlagManager.Instance.AddFlag("Progress9");
        }
        else
        {
            FlagManager.Instance.AddFlag("Progress8");
        }
    }
}
