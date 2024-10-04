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

    private InputSetting _inputSetting;
    private int cursorPlace;
    [SerializeField] private Sprite cursorSprite;  //カーソル画像
    [SerializeField] private Sprite questionBranchSprite;  //非選択時の画像

    void Start()
    {
        _inputSetting = InputSetting.Load();
        questionBranchImage = new Image[questionBranches.Length];
        for (int i = 0; i < questionBranches.Length; i++)
        {
            questionBranches[i].SetActive(false);
            questionBranchImage[i] = questionBranches[i].GetComponent<Image>();
        }
        cursorPlace = 0;
    }
    public void DisplayQuestion(string[] words)
    {
        for (int i = 0; i < words.Length; i++)
        {
            if (!words[i].StartsWith("[question]")) continue;  //[question]タグを探す  仮フォーマット: ans1^output1|ans2^output2|ans3^output3

            TextConverter textConverter = new TextConverter();
            string[][] word = textConverter.Converter(words[i]);
            QuestionBranch(word[0]);
            CursorMove(Mathf.Min(word[0].Length, questionBranches.Length));
            if (_inputSetting.GetDecideKeyDown())
            {
                DebugLogger.Log(word[1][cursorPlace]);  //仮の出力
            }
            return;
        }
        for (int i = 0; i < questionBranches.Length; i++)
        {
            questionBranches[i].SetActive(false);
        }
    }

    private void QuestionBranch(string[] str)
    {
        int max = Mathf.Min(str.Length, questionBranches.Length);  //テキストで指定された選択肢の数と、設置した選択肢の数の小さい方
        for (int i = 0; i < max; i++)
        {
            questionBranches[i].SetActive(true); //選択肢の表示
            textMeshPro[i].text = str[i];
        }
    }

    private void CursorMove(int max)
    {
        if (cursorPlace >= 0 && cursorPlace < max) 
        {
            if (_inputSetting.GetBackKeyUp() || _inputSetting.GetRightKeyUp())  //一直線に並べることを想定
            {
                cursorPlace++;
                if (cursorPlace >= max) cursorPlace = max - 1;
            }
            else if (_inputSetting.GetForwardKeyUp() || _inputSetting.GetLeftKeyUp())
            {
                cursorPlace--;
                if (cursorPlace < 0) cursorPlace = 0;
            }
        }
        for (int i = 0; i < max; ++i)
        {
            questionBranchImage[i].sprite = questionBranchSprite;
        }
        questionBranchImage[cursorPlace].sprite = cursorSprite;
    }
}
