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
    [SerializeField] private TimingBar timingBar;
    [SerializeField] private TextMeshProUGUI tmp;
    private InputSetting _inputSetting;

    [SerializeField] private int repeat;
    private int attempt = 1;                    //現在の試行回数
    private float timeSchedule;                 //時間制御用

    [SerializeField] private string nextScene;
    private string score;                       //判定表示
    private List<float> timingResults = new();
    [HideInInspector] public float timingResult;

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
        timingSlider.SliderMove();
        if (timeSchedule >= 0)
        {
            if (_inputSetting.GetDecideKeyDown())
            {
                SaveScore();
                timingSlider.SliderStop();
                if (JustTimingDiff() < 0)
                {
                    ScoreLetter("<size=80%><color=#00FFFF>fast</color></size><br>");
                }
                else if (JustTimingDiff() > 0)
                {
                    ScoreLetter("<size=80%><color=#FFFF00>slow</color></size><br>");
                }
                else
                {
                    tmp.text = "<color=#FF00FF>just!</color>";
                }
                timeSchedule = -2.0f;
            }
        }else if(timeSchedule > -0.1f && timeSchedule <0)
        {
            EndTimingGame(repeat);
        }
    }

    private void Initialize()
    {
        timingSlider.Initialize();
        timingBar.Initialize();
        tmp.text = "";
    }

    private float JustTimingDiff()  //判定とのずれ(座標)を返す
    {
        return timingSlider.timingSlider.value* timingSlider.height - timingBar.justTiming;
    }

    private void ScoreLetter(string text)  //精度を表示する
    {
        score = text;
        if (Math.Abs(JustTimingDiff()) < 25) score += "<color=#7FFF00>Great</color>";
        else if (Math.Abs(JustTimingDiff()) < 50) score += "<color=#00FF7F>Good</color>";
        else score += "<color=#0000FF>Bad</color>";
        tmp.text = score;
    }

    private void EndTimingGame(int rep)  //スライダーが停止された後の振る舞いを指定 rep:タイミングゲーを繰り返す回数
    {
        if(attempt < rep)
        {
            attempt++;
            Initialize();
            timingSlider.ReInitialize();
            timeSchedule = 0;
        }
        else  //タイミングゲーの後は会話ウィンドウに遷移する
        {
            CalcScoreAverage();
            Debug.Log(timingResult);
            SceneManager.LoadScene(nextScene);
        }
    }

    private void SaveScore()  //判定とのずれ(座標)を絶対値で保存する
    {
        timingResults.Add(Math.Abs(JustTimingDiff()));
    }

    private void CalcScoreAverage()  //タイミングゲーの成績でテキストが変化するらしいのでtimingResultsの平均値を計算しておく
    {
        timingResult = timingResults.Average();
    }
}
