using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimingGameManager : MonoBehaviour
{
    [SerializeField] TimingGame timingGame;
    [SerializeField] GameObject timingGameObject;

    private void Start()
    {
        Initialize();
    }
    void Update()
    {
        if (!(FlagManager.Instance.HasFlag("StartTimingGame1") ^ FlagManager.Instance.HasFlag("StartTimingGame2")))
        {
            EndTimingGame();
        }
    }

    public void Initialize()
    {
        timingGameObject.SetActive(true);
        timingGame.Initialize();
    }

    private void EndTimingGame()
    {
        timingGameObject.SetActive(false);
        DebugLogger.Log($"{FlagManager.Instance.HasFlag("Progress9")}:{FlagManager.Instance.HasFlag("Progress8")}");
        SceneManager.LoadScene("reference_room");
    }
}
