using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTextManager : MonoBehaviour
{
    [SerializeField] private MainTextDrawer mainTextDrawer;
    [SerializeField] private NameTextDrawer nameTextDrawer;
    [SerializeField] private ChangeBackground changeBackground;
    [SerializeField] private Question question;
    [SerializeField] private Pause pause;
    [SerializeField] private ChangeSound changeSound;
    [SerializeField] private float intervalTime;
    private float unitTime;
    private InputSetting _inputSetting;

    private int lineNumber;
    TalkData talkData;
    private string fileName = "nantokaKaiwa";

    // Start is called before the first frame update
    void Start()
    {
        _inputSetting = InputSetting.Load();
        Initiallize();
    }

    // Update is called once per frame
    void Update()
    {
        unitTime += Time.deltaTime;
        
        if (unitTime >= intervalTime)
        {
            unitTime -= intervalTime * mainTextDrawer.GetDelayTime();
            mainTextDrawer.Typewriter();
        }

        // zキーが離されたとき、次の行へ移動
        if (_inputSetting.GetDecideKeyUp() || _inputSetting.GetCancelKeyUp())
        {
            if (mainTextDrawer.AllowChangeLine() && unitTime > -0.45f)
            {
                //次の行へ移動し、表示する文字数をリセット
                if (_inputSetting.GetDecideKeyUp() && lineNumber < talkData.Content.Length - 1)
                {
                    ChangeLine(1);
                    DisplayText();
                    DebugLogger.Log("NextLine");
                }
                else if (_inputSetting.GetCancelKeyUp() && 0 < lineNumber)
                {
                    ChangeLine(-1);
                    DisplayText();
                    DebugLogger.Log("BackLine");
                }
                else
                {
                    gameObject.SetActive(false);
                    pause.UnPauseAll();
                    return;
                }
            }
            else if (unitTime > -0.45f)
            {
                mainTextDrawer.SkipTypewriter();
            }
            else
            {
                //エラー対策。不要説はある。
                unitTime = 0.2f;
            }
            if (unitTime > -0.55f)//連打対策（爆速スクロール等）
                unitTime -= 0.35f;
        }
        if (_inputSetting.GetBackKeyUp() || _inputSetting.GetRightKeyUp())
        {
            question.QuestionCursorMove(1);
        }
        else if (_inputSetting.GetForwardKeyUp() || _inputSetting.GetLeftKeyUp())
        {
            question.QuestionCursorMove(-1);
        }

        //次の行へ進むアイコンの表示非表示
        mainTextDrawer.NextLineIcon();
    }

    public void Initiallize()
    {
        pause.PauseAll();
        mainTextDrawer.Initialize();
        nameTextDrawer.Initialize();
        lineNumber = 0;
        unitTime = 0f;

        string filePath = string.Join('/', Application.streamingAssetsPath,"TalkData", fileName+".json");
        talkData = SaveUtility.JsonToData<TalkData>(filePath);

        //テキストを表示
        DisplayText();
    }

    public void SetJsonFileName(string jsonFileName)
    {
        fileName = jsonFileName;
    }

    private void DisplayText()
    {
        //前の行の名前欄や選択肢を非表示にしておく
        nameTextDrawer.DisableNameText();
        question.InitializeQuestionBranch();
        TextTagShifter();
    }

    private void TextTagShifter()
    {
        if (talkData.Content[lineNumber].Speaker != null) 
        {
            nameTextDrawer.DisplayNameText(talkData.Content[lineNumber].Speaker);
        }
        if (talkData.Content[lineNumber].ChangeImage != null)
        {
            changeBackground.ChangeImages(talkData.Content[lineNumber].ChangeImage);
        }
        if (talkData.Content[lineNumber].QuestionData != null)
        {
            question.DisplayQuestion(talkData.Content[lineNumber].QuestionData);
        }
        if (talkData.Content[lineNumber].BGM != null)
        {
            changeSound.ChangeBGM(talkData.Content[lineNumber].BGM);
        }
        if (talkData.Content[lineNumber].SE != null)
        {
            changeSound.ChangeSE(talkData.Content[lineNumber].SE);
        }
        if (talkData.Content[lineNumber].Text != null)
        {
            mainTextDrawer.DisplayMainText(talkData.Content[lineNumber].Text);
            mainTextDrawer.DisplayTextRuby();
        }
    }

    private void ChangeLine(int increase)
    {
        unitTime = intervalTime;
        lineNumber += increase;
        mainTextDrawer.InitializeLine();
    }
}