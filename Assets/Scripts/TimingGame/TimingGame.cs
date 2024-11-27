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

    [SerializeField] TextMeshProUGUI AttemptNumber;  //試行回数表示用

    private int timingGameNumber;

    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] ItemInventory itemInventory;


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
        justTiming = timingBar.anchoredPosition.y / timingSlider.SliderCoordinateSpeed();  //判定の基準となる時間
        timeSchedule = -0.04f;
        attempt = 1;
        DisplayAttemptNumberText();
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
            switch (timingGameNumber)
            {
                case 1:
                    ItemDelete("虫入り瓶");
                    ItemDelete("レイの血");
                    ItemDelete("ナニカの肉");
                    break;
                case 2:
                    ItemDelete("テッセンの内臓");
                    break;
            }
            if (CalcScoreAverage() <= judgeGood)
            {
                if (CalcScoreAverage() <= judgeGreat)
                {
                    ConversationTextManager.Instance.InitializeFromString("とてもうまくできた。");
                }
                else
                {
                    ConversationTextManager.Instance.InitializeFromString("少しミスしたが、問題ない。");
                }
                ChangeSuccessFlag("Progress9");
            }
            else
            {
                ConversationTextManager.Instance.InitializeFromString("…………………………………………");
                ChangeSuccessFlag("Progress8");
            }
            DebugLogger.Log($"result:{Convert.ToString(CalcScoreAverage())}:{timingGameNumber}");
            FlagManager.Instance.DeleteFlag("StartTimingGame1");
            FlagManager.Instance.DeleteFlag("StartTimingGame2");
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

    public void ChangeSuccessFlag(string flagName)
    {
        if (timingGameNumber == 2)
        {
            FlagManager.Instance.AddFlag(flagName);
        }
    }

    private void ItemDelete(string itemName)
    {
        Item item = itemDatabase.GetItem(itemName);
        if (itemInventory.IsContains(item))
        {
            DebugLogger.Log(itemName);
            itemInventory.Remove(item);
        }
    }
}
