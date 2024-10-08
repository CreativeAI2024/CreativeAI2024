using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    [SerializeField] private QuestionBranch[] questionBranches;
    [SerializeField] private TextWindowCursor cursor;

    string[][] word;
    private bool thinkingTime;
    private int cursorMax;

    void Start()
    {
        RectTransform[] rectTransforms = new RectTransform[questionBranches.Length];
        thinkingTime = false;
        for (int i = 0; i < questionBranches.Length; i++)
        {
            rectTransforms[i] = questionBranches[i].GetComponent<RectTransform>();
        }
        cursor.SetRectTransform(rectTransforms);
    }

    public void DisplayQuestion(string words)
    {
        TextConverter textConverter = new TextConverter();
        word = textConverter.Converter(words);
        cursorMax = Mathf.Min(word[0].Length, questionBranches.Length);
        for (int i = 0; i < cursorMax; i++)
        {
            questionBranches[i].DisplayQuestionBranchText(word[0][i]);
        }
        cursor.ReactivateCursor();
        thinkingTime = true;
    }
    
    public void QuestionCursor()
    {
        cursor.CursorMove(cursorMax);
    }

    public void InitializeQuestionBranch()
    {
        for (int i = 0; i < questionBranches.Length; i++)
        {
            questionBranches[i].HideQuestionBranch();
        }
        cursor.HideCursor();
        thinkingTime = false;
    }

    public void QuestionOutput()
    {
        if (thinkingTime)
        {
            DebugLogger.Log(word[1][cursor.GetCursorPlace()]);  //仮の出力
        }
    }
}
