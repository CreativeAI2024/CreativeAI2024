using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class TimingGame : MonoBehaviour
{
    [SerializeField] private TimingSlider timingSlider;
    [SerializeField] private RectTransform timingBar;
    [SerializeField] private TextMeshProUGUI judgeText;
    private InputSetting _inputSetting;

    [SerializeField] private int repeat;
    private int attempt;                    //現在の試行回数
    private float timeSchedule;                 //時間制御用

    //[SerializeField] private string nextScene;
    private List<float> timingResults;

    [SerializeField, HeaderAttribute("単位:ms")] private float judgeGreat;  //判定その１
    [SerializeField] private float judgeGood;   //判定その２
    private float justTiming;

    
    private bool initializeFlag = false;

    /*private void Start()
    {
        Initialize();
    }*/

    void Update()
    {
        if (!initializeFlag) return;

        
        if (timeSchedule >= 0)
        {
            if (_inputSetting.GetDecideKey())
            {
                timingSlider.AscendSlider();
                timeSchedule += Time.deltaTime;
            }
            if (_inputSetting.GetDecideKeyUp())
            {
                SaveScore();
                timingSlider.SliderStop();
                JudgeTextTiming();
                timeSchedule = -2.0f;
            }
        }else if(-0.1f < timeSchedule && timeSchedule < -0.05f)
        {
            EndTimingGame();
            timeSchedule += Time.deltaTime;
        }
        else
        {
            if (!_inputSetting.GetDecideKey())
            {
                timeSchedule += Time.deltaTime;
            }
        }
    }

    public void Initialize()
    {
        if (initializeFlag)
            return;

        _inputSetting = InputSetting.Load();
        timingResults = new();
        initializeFlag = true;
        timingSlider.Initialize();
        justTiming = timingBar.anchoredPosition.y / timingSlider.SliderCoordinateSpeed();  //判定の基準となる時間
        judgeText.text = "";
        timeSchedule = -0.04f;
        attempt = 1;
    }

    public bool GetInitializeFlag()
    {
        return initializeFlag;
    }

    private float JustTimingDiff()  //判定とのずれ(時間)を返す
    {
        return timingSlider.SliderTopPositionTime() - justTiming;
    }

    private float JustTimingDiffAbs() //判定とのずれ(時間)を絶対値で返す
    {
        return Math.Abs(JustTimingDiff());
    }

    private void JudgeTextTiming()  //基準に対し早いか遅いかを表示する 
    {
        if (JustTimingDiff() < 0)
        {
            JudgeTextAccuracy("<size=80%><color=#00FFFF>fast</color></size><br>");
        }
        else if (JustTimingDiff() > 0)
        {
            JudgeTextAccuracy("<size=80%><color=#FFFF00>slow</color></size><br>");
        }
        else
        {
            judgeText.text = "<color=#FF00FF>just!</color>";
        }
    }

    private void JudgeTextAccuracy(string text)  //精度を表示する
    {
        if (JustTimingDiffAbs() < judgeGreat) text += "<color=#7FFF00>Great</color>";
        else if (JustTimingDiffAbs() < judgeGood) text += "<color=#00FF7F>Good</color>";
        else text += "<color=#0000FF>Bad</color>";
        judgeText.text = text;
    }

    private void EndTimingGame()  //スライダーが停止された後の振る舞いを指定 rep:タイミングゲーを繰り返す回数
    {
        if(attempt < repeat)
        {
            attempt++;
            judgeText.text = "";
            timingSlider.RestartSlider();
            timeSchedule = 0.0f;
        }
        else  
        {
            //DebugLogger.Log(CalcScoreAverage());
            initializeFlag = false;
            ConversationTextManager.Instance.InitializeFromString(Convert.ToString(CalcScoreAverage()));
        }
    }

    private void SaveScore()  //判定とのずれ(時間)を絶対値で保存する
    {
        timingResults.Add(JustTimingDiffAbs());
    }

    public float CalcScoreAverage()  //タイミングゲーの成績でテキストが変化するらしいのでtimingResultsの平均値を計算しておく
    {
        return timingResults.Average();
    }
}
