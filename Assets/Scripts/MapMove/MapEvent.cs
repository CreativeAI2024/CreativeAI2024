using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapEvent : MonoBehaviour
{
    public Vector2Int eventTilePosition;
    [FormerlySerializedAs("playerTileInfo")] [FormerlySerializedAs("tilePosition")] [SerializeField] private TileInfo tileInfo;
    [SerializeField] private ConversationTextManager _conversationTextManager;

    private InputSetting _inputSetting;
    // Start is called before the first frame update
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    
    public void Initialize()
    {
        // mapイベント情報を生成
    }

    // Update is called once per frame
    void Update()
    {
        // 隣接するマスであったら
        if (Vector2.Distance(tileInfo.GridPosition, eventTilePosition) <= 1 && _inputSetting.GetDecideKeyDown())
        {
            ConversationEvent();
        }
    }

    public void ConversationEvent()
    {
        _conversationTextManager.gameObject.SetActive(true);
        _conversationTextManager.Initiallize();
    }
}

