using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class LongPressGameMain : MonoBehaviour
{
    [SerializeField] private float goalTime;
    private float pressedTime;
    private float scheduleTime;
    private InputSetting _inputSetting;

    [SerializeField] private LongPressGameSlider longPressGameSlider;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        scheduleTime += Time.deltaTime;
        if (!(longPressGameSlider == null))
        {
            UIControl();
        }

        if (pressedTime >= goalTime && _inputSetting.GetDecideKeyUp())
        {
            Debug.Log("success");
        }
        else if(pressedTime < goalTime && _inputSetting.GetDecideKeyUp())
        {
            Debug.Log("fail");
            pressedTime = 0;
        }
        else if(pressedTime < goalTime)
        {
            Proceed();
        }
    }

    public void Initialize()
    {
        _inputSetting = InputSetting.Load();
        scheduleTime = 0;
        pressedTime = 0;
    }
    public void Proceed()
    {
        if (_inputSetting.GetDecideKey())
        {
            pressedTime += Time.deltaTime;
        }
    }

    public void UIControl()
    {
        longPressGameSlider.SetSliderValue(pressedTime / goalTime);
        longPressGameSlider.SetGoalTimeText(goalTime.ToString());
        longPressGameSlider.SetPressCountText(pressedTime.ToString());
    }
}
