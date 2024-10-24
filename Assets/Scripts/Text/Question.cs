using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    [SerializeField] private QuestionBranch[] questionBranches;
    [SerializeField] private TextWindowCursor cursor;
    RectTransform[] rectTransforms;
    //string[][] word;
    private int cursorMax;
    private int cursorPlace;

    void Start()
    {
        rectTransforms = new RectTransform[questionBranches.Length];
        for (int i = 0; i < questionBranches.Length; i++)
        {
            rectTransforms[i] = questionBranches[i].GetComponent<RectTransform>();
        }
    }

    /*public void DisplayQuestion(string words)
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
    }*/
    public void DisplayQuestion(QuestionData[] questionData)
    {
        //TextConverter textConverter = new TextConverter();
        //word = textConverter.Converter(words);
        cursorMax = Mathf.Min(questionData.Length, questionBranches.Length);
        for (int i = 0; i < cursorMax; i++)
        {
            questionBranches[i].SetVisibleQuestionBranch(true);
            questionBranches[i].QuestionBranchText(questionData[i].Answer);
        }
        cursorPlace = 0;
        cursor.SetVisibleCursor(true);
        cursor.CursorMove(rectTransforms[cursorPlace].position);
    }

    public void QuestionCursorMove(int increase)
    {
        cursorPlace = Mathf.Clamp(cursorPlace + increase, 0, cursorMax - 1);
        cursor.CursorMove(rectTransforms[cursorPlace].position);
    }

    public void InitializeQuestionBranch()
    {
        for (int i = 0; i < questionBranches.Length; i++)
        {
            questionBranches[i].SetVisibleQuestionBranch(false);
        }
        cursor.SetVisibleCursor(false);
    }

    public void QuestionOutput(int[] questionOutput)
    {
        //DebugLogger.Log("output!");  //仮の出力
        DebugLogger.Log(questionOutput[cursorPlace]);
    }
}
