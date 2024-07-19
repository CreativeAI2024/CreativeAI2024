using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class LongPressGameMain : MonoBehaviour
{
    [SerializeField, HeaderAttribute("’PˆÊ:s")] private float goalTime;
    private float pressedTime;
    private InputSetting _inputSetting;

    [SerializeField] private LongPressGameSlider longPressGameSlider;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (!(longPressGameSlider == null))
        {
            UIControl();
        }

        if (pressedTime >= goalTime && _inputSetting.GetDecideKeyUp())
        {
            DebugLogger.Log("success");
            pressedTime = 0;
        }
        else if(pressedTime < goalTime && _inputSetting.GetDecideKeyUp())
        {
            DebugLogger.Log("fail");
            pressedTime = 0;
        }
        else if(pressedTime < goalTime)
        {
            Proceed();
        }
    }

    private void Initialize()
    {
        _inputSetting = InputSetting.Load();
        pressedTime = 0;
    }
    private void Proceed()
    {
        if (_inputSetting.GetDecideKey())
        {
            pressedTime += Time.deltaTime;
        }
    }

    private void UIControl()
    {
        longPressGameSlider.SetSliderValue(pressedTime / goalTime);
        longPressGameSlider.SetGoalTimeText(goalTime.ToString());
        longPressGameSlider.SetPressCountText(pressedTime.ToString());
    }
}
