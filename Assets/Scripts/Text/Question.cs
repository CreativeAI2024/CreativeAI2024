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

        // QuestionPanelの下の辺をStartとしている。
        // y軸の向きは上である。
        float branchStartPosition = 200;
        float halfQuestionPanelHeight = 400 / 2;
        // float questionBranchSpacing = 250 / questionLength; //係数250で反比例の式にすると選択肢の間隔がいい感じになる
        float questionBranchSpacing = 100;
        // indexを渡すと、下揃えのbranchのPositionを返す。(i=0は一番下のbranch)
        Func<int, float> bottomAlignmentBrchPos = (i) => branchStartPosition + i * questionBranchSpacing;
        // branchPositionをPanelの中央までずらすのに必要なoffset
        float offsetToCenter = halfQuestionPanelHeight - ((bottomAlignmentBrchPos(questionLen - 1) - bottomAlignmentBrchPos(0)) / 2f);
        for (int i = 0; i < questionLen; i++)
        {
            int reverseIndex = questionLen - 1 - i;
            float positionX = rectTransforms[reverseIndex].anchoredPosition.x;
            rectTransforms[reverseIndex].anchoredPosition = new(positionX, bottomAlignmentBrchPos(i) + offsetToCenter);
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
