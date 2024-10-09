using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    [SerializeField] private QuestionBranch[] questionBranches;
    [SerializeField] private TextWindowCursor cursor;
    RectTransform[] rectTransforms;
    private InputSetting _inputSetting;
    string[][] word;
    private bool thinkingTime;
    private int cursorMax;
    private int cursorPlace;

    void Start()
    {
        rectTransforms = new RectTransform[questionBranches.Length];
        thinkingTime = false;
        cursorPlace = 0;
        for (int i = 0; i < questionBranches.Length; i++)
        {
            rectTransforms[i] = questionBranches[i].GetComponent<RectTransform>();
        }
        _inputSetting = InputSetting.Load();
    }

    public void DisplayQuestion(string words)
    {
        TextConverter textConverter = new TextConverter();
        word = textConverter.Converter(words);
        cursorMax = Mathf.Min(word[0].Length, questionBranches.Length);
        for (int i = 0; i < cursorMax; i++)
        {
            questionBranches[i].SetVisibleQuestionBranch(true);
            questionBranches[i].QuestionBranchText(word[0][i]);
        }
        cursorPlace = 0;
        cursor.SetVisibleCursor(true);
        cursor.CursorMove(rectTransforms[cursorPlace].position);
        thinkingTime = true;
    }
    
    public void QuestionCursor(int increase)
    {
        CursorPlaceChange(increase);
        cursor.CursorMove(rectTransforms[cursorPlace].position);
    }

    private void CursorPlaceChange(int increase)
    {//一直線に並べることを想定
        if (cursorPlace + increase >= 0 && cursorPlace + increase < cursorMax)
        {
            cursorPlace += increase;
        }
    }



    public void InitializeQuestionBranch()
    {
        for (int i = 0; i < questionBranches.Length; i++)
        {
            questionBranches[i].SetVisibleQuestionBranch(false);
        }
        cursor.SetVisibleCursor(false);
        thinkingTime = false;
    }

    public void QuestionOutput()
    {
        if (thinkingTime)
        {
            DebugLogger.Log(word[1][cursorPlace]);  //仮の出力
        }
    }
}
