using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperGameManager : DontDestroySingleton<PaperGameManager>
{
    [SerializeField] GameObject paperGameObject;
    private InputSetting _inputSetting;
    [SerializeField] PaperMove paperMove;

    public override void Awake()
    {
        base.Awake();
        _inputSetting = InputSetting.Load();
    }
    // Update is called once per frame
    void Update()
    {
        if (_inputSetting.GetCancelKeyDown())
        {
            paperGameObject.SetActive(false);
        }
    }

    public void Initialize()
    {
        paperGameObject.SetActive(true);
        paperMove.Initialize();
    }
}
