using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    [SerializeField] private QuestionBranch[] questionBranches;
    [SerializeField] private TextWindowCursor cursor;
    [SerializeField] private GameObject questionPanel;
    private RectTransform[] rectTransforms;
    private int cursorMax;
    private int cursorPlace;

    public void Initialize()
    {
        rectTransforms = new RectTransform[questionBranches.Length];
        for (int i = 0; i < questionBranches.Length; i++)
        {
            rectTransforms[i] = questionBranches[i].GetComponent<RectTransform>();
        }
    }

    public void DisplayQuestion(QuestionData[] questionData)
    {
        int questionLen = questionData.Length;
        cursorMax = Mathf.Min(questionLen, questionBranches.Length);
        for (int i = 0; i < cursorMax; i++)
        {
            questionBranches[i].SetVisibleQuestionBranch(true);
            questionBranches[i].QuestionBranchText(questionData[i].Answer);
        }

        float branchCenterPos = 410;
        float branchAreaHeight = questionLen * 75; //75でかけるといい感じになる
        float branchStartPos = branchCenterPos + branchAreaHeight / 2;
        float branchSpacing = branchAreaHeight / (questionLen - 1);
        for (int i = 0; i < questionLen; i++)
        {
            rectTransforms[i].anchoredPosition = new(rectTransforms[i].anchoredPosition.x, branchStartPos - branchSpacing * i);
        }

        cursorPlace = 0;
        questionPanel.SetActive(true);
        cursor.SetVisibleCursor(true);
        cursor.CursorMove(rectTransforms[cursorPlace].position);
    }

    public void QuestionCursorMove(int increase)
    {
        if (cursorMax - 1 <= 0)
            cursorMax = 1;

        cursorPlace = Mathf.Clamp(cursorPlace + increase, 0, cursorMax - 1);
        cursor.CursorMove(rectTransforms[cursorPlace].position);
    }

    public void InitializeQuestionBranch()
    {
        for (int i = 0; i < questionBranches.Length; i++)
        {
            questionBranches[i].SetVisibleQuestionBranch(false);
        }
        questionPanel.SetActive(false);
        cursor.SetVisibleCursor(false);
    }

    public int GetCursorPlace()
    {
        return cursorPlace;
    }
}
