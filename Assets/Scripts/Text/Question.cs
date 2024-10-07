using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [SerializeField] private QuestionBranch[] questionBranches;
    [SerializeField] private TextWindowCursor cursor;

    string[][] word;
    private bool thinkingTime;
    private int cursorMax;

    void Start()
    {
        GameObject[] gameObjectCarrier = new GameObject[questionBranches.Length];
        thinkingTime = false;
        for (int i = 0; i < questionBranches.Length; i++)
        {
            gameObjectCarrier[i] = questionBranches[i].GetGameObjects();
        }
        cursor.SetGameObject(gameObjectCarrier);
    }

    public void DisplayQuestion(string words)
    {
        TextConverter textConverter = new TextConverter();
        word = textConverter.Converter(words);
        cursorMax = Mathf.Min(word[0].Length, questionBranches.Length);
        for (int i = 0; i < cursorMax; i++)
        {
            questionBranches[i].EnableQuestionBranch(word[0][i]);
        }
        cursor.EnableCursor();
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
            questionBranches[i].DisableQuestionBranch();
        }
        cursor.DisableCursor();
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
