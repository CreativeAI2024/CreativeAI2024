using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapEvent : MonoBehaviour
{
    public Vector2Int eventTilePosition;
    [SerializeField] private PlayerController player;
    [SerializeField] private Pause pause;
    [SerializeField] private string talkJsonName;

    private InputSetting _inputSetting;
    // Start is called before the first frame update
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!ConversationTextManager.Instance.GetInitializeFlag())
        {
            // 隣接するマスであったら
            if (Vector2.Distance(player.GetPlayerGridPosition(), eventTilePosition) <= 1 && _inputSetting.GetDecideKeyDown())
            {
                ConversationEvent();
            }
            else
            {
                pause.UnPauseAll();
            }
        }
    }

    public void ConversationEvent()
    {
        pause.PauseAll();
        ConversationTextManager.Instance.Initialize(talkJsonName);
    }
}
