using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Linq;

public class TimingGame : MonoBehaviour
{
    [SerializeField] private TimingSlider timingSlider;
    private InputSetting _inputSetting;

    [SerializeField] private int repeat;
    private int attempt;                    //現在の試行回数
    private float timeSchedule;                 //時間制御用

    private List<float> timingResults;
    [SerializeField] TimingGameJudge timingGameJudge;
    [SerializeField] TextMeshProUGUI AttemptNumber;  //試行回数表示用

    private int timingGameNumber;
    [SerializeField] TimingGameItem timingGameItem;

    void Update()
    {
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
        //itemDatabase.Initialize();
        //itemInventory.Initialize();
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
        timingSlider.Initialize();
        timeSchedule = -0.04f;
        attempt = 1;
        DisplayAttemptNumberText();
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
            timingGameItem.ConsumeItem(timingGameNumber);
            timingGameJudge.JudgeScore(CalcScoreAverage());
            DebugLogger.Log($"result:{Convert.ToString(CalcScoreAverage())}:{timingGameNumber}");
            FlagManager.Instance.DeleteFlag("StartTimingGame1");
            FlagManager.Instance.DeleteFlag("StartTimingGame2");
        }
    }

    private void SaveScore()  //判定とのずれ(時間)を絶対値で保存する
    {
        timingResults.Add(timingSlider.JustTimingDiffAbs());
    }

    private float CalcScoreAverage()  //タイミングゲーの成績でテキストが変化するらしいのでtimingResultsの平均値を計算しておく(ミリ秒)
    {
        return timingResults.Average() * 1000;
    }

    private void DisplayAttemptNumberText()
    {
        AttemptNumber.text = $"Attempt:<br><size=150>{attempt}/{repeat}</size>";
    }
}
