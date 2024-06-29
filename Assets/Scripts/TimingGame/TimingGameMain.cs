using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class TimingGameMain : MonoBehaviour
{
    [SerializeField] private TimingSlider timingSlider;
    [SerializeField] private RectTransform timingBar;
    [SerializeField] private TextMeshProUGUI judgeText;
    private InputSetting _inputSetting;

    [SerializeField] private int repeat;
    private int attempt = 1;                    //現在の試行回数
    private float timeSchedule;                 //時間制御用

    [SerializeField] private string nextScene;
    private List<float> timingResults = new();

    [SerializeField, HeaderAttribute("単位:ms")] private float judgeGreat;  //判定その１
    [SerializeField] private float judgeGood;   //判定その２
    private float justTiming;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        _inputSetting = InputSetting.Load();
    }

    // Update is called once per frame
    void Update()
    {
        timeSchedule += Time.deltaTime;
        if (timeSchedule >= 0)
        {
            if (_inputSetting.GetDecideKeyDown())
            {
                SaveScore();
                timingSlider.SliderStop();
                JudgeTextTiming();
                timeSchedule = -2.0f;
            }
        }else if(-0.1f < timeSchedule && timeSchedule < 0)
        {
            EndTimingGame();
        }
    }

    private void Initialize()
    {
        timingSlider.Initialize();
        justTiming = timingBar.anchoredPosition.y / timingSlider.SliderCoordinateSpeed();  //判定の基準となる時間
        judgeText.text = "";
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
            timeSchedule = 0;
        }
        else  //タイミングゲーの後は会話ウィンドウに遷移する
        {
            Debug.Log(CalcScoreAverage());
            SceneManager.LoadScene(nextScene);
        }
    }

    private void SaveScore()  //判定とのずれ(時間)を絶対値で保存する
    {
        timingResults.Add(JustTimingDiffAbs());
    }

    private float CalcScoreAverage()  //タイミングゲーの成績でテキストが変化するらしいのでtimingResultsの平均値を計算しておく
    {
        return timingResults.Average();
    }
}
