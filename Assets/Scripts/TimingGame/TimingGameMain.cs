using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class TimingGameMain : MonoBehaviour
{
    public TimingSlider timingSlider;
    public TimingBar timingBar;
    public TextMeshProUGUI tmp;
    [SerializeField] private int repeat;
    private int attempt = 1;
    public string nextScene;
    private InputSetting _inputSetting;
    private string score;
    private float timeSchedule;
    [HideInInspector] public List<float> timingResults = new();
    [HideInInspector] public float timingResult;
    // Start is called before the first frame update
    void Start()
    {
        timingSlider.Initialize();
        timingBar.Initialize();
        _inputSetting = InputSetting.Load();
        tmp.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        timeSchedule += Time.deltaTime;
        timingSlider.ObjectMove();
        if (timeSchedule >= 0)
        {
            if (_inputSetting.GetDecideKeyDown())
            {
                SaveScore();
                timingSlider.ObjectStop();
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

    public float JustTimingDiff()
    {
        return timingSlider.timingSlider.value* timingSlider.height - timingBar.justTiming;
    }

    public void ScoreLetter(string text)
    {
        score = text;
        if (Math.Abs(JustTimingDiff()) < 25) score += "<color=#7FFF00>Great</color>";
        else if (Math.Abs(JustTimingDiff()) < 50) score += "<color=#00FF7F>Good</color>";
        else score += "<color=#0000FF>Bad</color>";
        tmp.text = score;
    }

    public void EndTimingGame(int rep)
    {
        if(attempt < rep)
        {
            attempt++;
            timingSlider.Initialize();
            timingSlider.ReInitialize();
            timingBar.Initialize();
            tmp.text = "";
            timeSchedule = 0;
        }
        else
        {
            CalcScoreAverage();
            Debug.Log(timingResult);
            SceneManager.LoadScene(nextScene);
        }
    }

    public void SaveScore()
    {
        timingResults.Add(Math.Abs(JustTimingDiff()));
    }

    public void CalcScoreAverage()
    {
        timingResult = timingResults.Average();
    }
}
