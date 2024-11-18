using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingGameManager : DontDestroySingleton<TimingGameManager>
{
    [SerializeField] TimingGame timingGame;
    [SerializeField] private GameObject timingGameObject;
    bool initializeFlag = false;

    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if ((FlagManager.Instance.HasFlag("StartTimingGame1") ^ FlagManager.Instance.HasFlag("StartTimingGame2")) && !timingGame.GetInitializeFlag())
        {
            Initialize(); 
        }
        if (timingGame.GetEndFlag())
        {
            timingGameObject.SetActive(false);
            ConversationTextManager.Instance.OnConversationEnd += EndTimingGame;
        }
    }

    public void Initialize()
    {
        if (initializeFlag)
            return;

        initializeFlag = true;
        timingGameObject.SetActive(true);
        timingGame.Initialize();
    }

    public bool GetInitializeFlag()
    {
        return initializeFlag;
    }

    private void EndTimingGame()
    {
        initializeFlag = false;
        EndTimingGameFlag("StartTimingGame1", "SuccessTimingGame1");
        EndTimingGameFlag("StartTimingGame2", "SuccessTimingGame2");
        DebugLogger.Log($"{FlagManager.Instance.HasFlag("SuccessTimingGame1")}:{FlagManager.Instance.HasFlag("SuccessTimingGame2")}");
        FlagManager.Instance.DeleteFlag("StartTimingGame1");
        FlagManager.Instance.DeleteFlag("StartTimingGame2");
    }

    private void EndTimingGameFlag(string startFlagName, string addFlagName)
    {
        if (!FlagManager.Instance.HasFlag(startFlagName))
            return;

        if (timingGame.GetSuccessFlag())
        {
            FlagManager.Instance.AddFlag(addFlagName);
        }
        else
        {
            FlagManager.Instance.DeleteFlag(addFlagName);
        }
    }
}
