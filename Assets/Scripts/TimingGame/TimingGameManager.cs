using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingGameManager : DontDestroySingleton<TimingGameManager>
{
    [SerializeField] TimingGame timingGame;
    [SerializeField] private GameObject timingGameObject;
    InputSetting _inputSetting;
    bool initializeFlag = false;

    public override void Awake()
    {
        base.Awake();
        _inputSetting = InputSetting.Load();
    }

    void Update()
    { 
        if (!timingGame.GetInitializeFlag())
        {
            timingGameObject.SetActive(false);
            if (ConversationTextManager.Instance.OnConversationEnd)
            {
                initializeFlag = false;
            }
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

}
