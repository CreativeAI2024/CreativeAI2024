using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RushGameMain : MonoBehaviour
{
    [SerializeField] private float goalCount;
    private float rushedCount;
    [SerializeField] private float limitTime;
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
            if (rushedCount > goalCount)
            {
                Debug.Log("success");     //成功したとき
            }
            else
            {
                Debug.Log("fail");     //失敗したとき
            }
        }
    }

    public void Initialize()
    {
        _inputSetting = InputSetting.Load();
        ScheduleTime = 0;
    }
    public void Proceed()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            rushedCount++;
        }
    }

    public void UIControl()
    {
        rushGameSlider.SetSliderValue(rushedCount / goalCount);
        rushGameSlider.SetGoalCountText(goalCount.ToString());
        rushGameSlider.SetTimeLimit(ScheduleTime.ToString());
        rushGameSlider.SetPressCountText(rushedCount.ToString());
    }
}
