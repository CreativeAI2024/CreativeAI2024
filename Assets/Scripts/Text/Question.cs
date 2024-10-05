using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textMeshPro;
    [SerializeField] private GameObject[] questionBranches;
    private Image[] questionBranchImage;

    string[][] word;
    private bool thinkingTime;
    private int cursorMax;

    void Start()
    {
        
        questionBranchImage = new Image[questionBranches.Length];
        for (int i = 0; i < questionBranches.Length; i++)
        {
            questionBranches[i].SetActive(false);
            questionBranchImage[i] = questionBranches[i].GetComponent<Image>();
        }
        thinkingTime = false;
    }

    public void DisplayQuestion(string words)
    {
        TextConverter textConverter = new TextConverter();
        word = textConverter.Converter(words);
        QuestionBranch(word[0]);
        cursorMax = Mathf.Min(word[0].Length, questionBranches.Length);
    }
    public int GetCursorMax()
    {
        return cursorMax;
    }

    public bool GetThinkingTime()
    {
        return thinkingTime;
    }

    public void DisableQuestionBranches()
    {
        for (int i = 0; i < questionBranches.Length; i++)
        {
            questionBranches[i].SetActive(false);
            thinkingTime = false;
        }
    }

    public Image[] GetQuestionBranchImage() 
    { 
        return questionBranchImage; 
    }

    private void QuestionBranch(string[] str)
    {
        int max = Mathf.Min(str.Length, questionBranches.Length);  //テキストで指定された選択肢の数と、設置した選択肢の数の小さい方
        for (int i = 0; i < max; i++)
        {
            questionBranches[i].SetActive(true); //選択肢の表示
            textMeshPro[i].text = str[i];
        }
        thinkingTime = true;
    }

    public void QuestionOutput(int cursorPlace)
    {
        DebugLogger.Log(word[1][cursorPlace]);  //仮の出力
    }
}
