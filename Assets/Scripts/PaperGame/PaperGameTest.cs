using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PaperGameTest : MonoBehaviour
{
    public Vector2Int eventTilePosition;
    [FormerlySerializedAs("tileInfo")][SerializeField] private PlayerController player;
    [SerializeField] private Pause pause;

    private InputSetting _inputSetting;
    // Start is called before the first frame update
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }

    // Update is called once per frame
    void Update()
    {
        // 隣接するマスであったら
        if (Vector2.Distance(player.GetGridPosition(), eventTilePosition) <= 1 && _inputSetting.GetDecideInputDown())
        {
            pause.PauseAll();
            PaperGameManager.Instance.Initialize();
        }
        else
        {
            pause.UnPauseAll();
        }

    }
}
