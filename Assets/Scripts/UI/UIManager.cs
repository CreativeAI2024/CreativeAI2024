using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject windowBox;
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetItemKeyDown())
        {
            ToggleWindow();
        }
    }
    public void ToggleWindow() //どのウィンドウを開けてるかの状態もリセットしたい。各ウィンドウのdisenable()の中で処理した方がいい？
    {
        ChangeActive(windowBox, !windowBox.activeSelf);
    }
    private void ChangeActive(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
