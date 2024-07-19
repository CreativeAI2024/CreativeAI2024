using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushGameMain : MonoBehaviour
{
    [SerializeField] private float goalCount;
    private float rushedCount;
    [SerializeField, HeaderAttribute("単位:s")] private float limitTime;
    private float ScheduleTime;
    private InputSetting _inputSetting;

    [SerializeField] private RushGameSlider rushGameSlider;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        ScheduleTime += Time.deltaTime;
        if (ScheduleTime < limitTime)
        {
            Proceed();
            if (!(rushGameSlider == null))
            {
                UIControl();
            }
        }
        else if(_inputSetting.GetDecideKeyDown())
        {
            if (rushedCount >= goalCount)
            {
                DebugLogger.Log("success");     //成功したとき
            }
            else
            {
                DebugLogger.Log("fail");     //失敗したとき
            }
        }
    }

    private void Initialize()
    {
        _inputSetting = InputSetting.Load();
        ScheduleTime = 0;
    }
    private void Proceed()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            rushedCount++;
        }
    }

    private void UIControl()
    {
        rushGameSlider.SetSliderValue(rushedCount / goalCount);
        rushGameSlider.SetGoalCountText(goalCount.ToString());
        rushGameSlider.SetTimeLimit(ScheduleTime.ToString());
        rushGameSlider.SetPressCountText(rushedCount.ToString());
    }
}
