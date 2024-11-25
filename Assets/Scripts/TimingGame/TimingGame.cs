using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Linq;

public class TimingGame : MonoBehaviour
{
    [SerializeField] private TimingSlider timingSlider;
    [SerializeField] private RectTransform timingBar;
    private InputSetting _inputSetting;

    [SerializeField] private int repeat;
    private int attempt;                    //現在の試行回数
    private float timeSchedule;                 //時間制御用

    private List<float> timingResults;

    [SerializeField, HeaderAttribute("単位:ms")] private float judgeGreat;  //判定その１
    [SerializeField] private float judgeGood;   //判定その２
    private float justTiming;
    private bool successFlag;

    [SerializeField] TextMeshProUGUI AttemptNumber;  //試行回数表示用

    private bool initializeFlag = false;
    bool endFlag = false;
    private int timingGameNumber;

    void Update()
    {
        if (!initializeFlag) return;

        if (timeSchedule >= 0)
        {
            if (_inputSetting.GetDecideInput())
            {
                timingSlider.AscendSlider();
                timeSchedule += Time.deltaTime;
            }
            if (_inputSetting.GetDecideInputUp())
            {
                SaveScore();
                timingSlider.SliderStop();
                timeSchedule = -2.0f;
            }
        }else if(-0.1f < timeSchedule && timeSchedule < -0.05f)
        {
            EndTimingGame();
            timeSchedule += Time.deltaTime;
            DisplayAttemptNumberText();
        }
        else
        {
            if (!_inputSetting.GetDecideInput())
            {
                timeSchedule += Time.deltaTime;
            }
        }
    }

    public void Initialize()
    {
        if (initializeFlag)
            return;

        if (FlagManager.Instance.HasFlag("StartTimingGame1"))
        {
            timingGameNumber = 1;
        }
        else if (FlagManager.Instance.HasFlag("StartTimingGame2"))
        {
            timingGameNumber = 2;
        }
        _inputSetting = InputSetting.Load();
        timingResults = new();
        initializeFlag = true;
        endFlag = false;
        timingSlider.Initialize();
        justTiming = timingBar.anchoredPosition.y / timingSlider.SliderCoordinateSpeed();  //判定の基準となる時間
        timeSchedule = -0.04f;
        successFlag = false;
        attempt = 1;
        DisplayAttemptNumberText();
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

    private void EndTimingGame()  //スライダーが停止された後の振る舞いを指定 rep:タイミングゲーを繰り返す回数
    {
        if(attempt < repeat)
        {
            attempt++;
            timingSlider.RestartSlider();
            timeSchedule = 0.0f;
        }
        else  
        {
            initializeFlag = false;
            if (CalcScoreAverage() <= judgeGood)
            {
                successFlag = true;
                if (CalcScoreAverage() <= judgeGreat)
                {
                    ConversationTextManager.Instance.InitializeFromString("とてもうまくできた。");
                }
                else
                {
                    ConversationTextManager.Instance.InitializeFromString("少しミスしたが、問題ない。");
                }
            }
            else
            {
                successFlag = false;
                ConversationTextManager.Instance.InitializeFromString("…………………………………………");
            }
            endFlag = true;
            DebugLogger.Log($"result:{Convert.ToString(CalcScoreAverage())}:{timingGameNumber}");
        }
    }

    private void SaveScore()  //判定とのずれ(時間)を絶対値で保存する
    {
        timingResults.Add(JustTimingDiffAbs());
    }

    private float CalcScoreAverage()  //タイミングゲーの成績でテキストが変化するらしいのでtimingResultsの平均値を計算しておく(ミリ秒)
    {
        return timingResults.Average() * 1000;
    }

    private void DisplayAttemptNumberText()
    {
        AttemptNumber.text = $"Attempt:<br><size=200>{attempt}/{repeat}</size>";
    }

    public bool GetSuccessFlag()
    {
        return successFlag;
    }

    public bool GetEndFlag()
    {
        return endFlag;
    }
}
