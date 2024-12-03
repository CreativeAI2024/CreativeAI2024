using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConversationTextManager : DontDestroySingleton<ConversationTextManager>
{
    [SerializeField] private MainTextDrawer mainTextDrawer;
    [SerializeField] private NameTextDrawer nameTextDrawer;
    [SerializeField] private ChangeBackground changeBackground;
    [SerializeField] private Question question;
    [SerializeField] private GameObject contentObject;
    [SerializeField] private float intervalTime;
    private float unitTime;
    private InputSetting _inputSetting;

    private int lineNumber;
    private bool initializeFlag = false;
    public event Action OnConversationStart { add => _onConversationStart += value; remove => _onConversationStart -= value; }
    private Action _onConversationStart;
    public event Action OnConversationEnd { add => _onConversationEnd += value; remove => _onConversationEnd -= value; }
    private Action _onConversationEnd;
    TalkData talkData;

    public override void Awake()
    {
        base.Awake();
        _inputSetting = InputSetting.Load();
        //InitializeFromString("nantokaKaiwa");
        //InitializeFromJson("parallelTest");
    }

    void Update()
    {
        if (!initializeFlag) return;

        unitTime += Time.deltaTime;

        if (unitTime >= intervalTime)
        {
            unitTime -= intervalTime * mainTextDrawer.GetDelayTime();
            mainTextDrawer.Typewriter();
        }

        if (_inputSetting.GetDecideInputUp() || _inputSetting.GetCancelKeyUp())
        {
            if (mainTextDrawer.AllowChangeLine() && unitTime > -0.45f)
            {
                //次の行へ移動し、表示する文字数をリセット
                if (_inputSetting.GetDecideInputUp() && lineNumber < talkData.Content.Length - 1)
                {
                    ChangeQuestionData();
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
                    SoundManager.Instance.StopBGM();
                    ChangeQuestionData();
                    EndConversation();
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

    public void InitializeFromString(string text)
    {
        talkData = new TalkData();
        talkData.Content = new Content[1];
        talkData.Content[0] = new Content();
        talkData.Content[0].Text = text;
        Initialize();
    }

    public void InitializeFromJson(string fileName)
    {
        string filePath = string.Join('/', Application.streamingAssetsPath, "TalkData", fileName + ".json");
        talkData = SaveUtility.JsonToData<TalkData>(filePath);
        Initialize();
    }

    private void Initialize()
    {
        if (initializeFlag)
            return;

        initializeFlag = true;
        _onConversationStart?.Invoke();
        contentObject.SetActive(true);

        lineNumber = 0;
        unitTime = -1f;
        mainTextDrawer.Initialize();
        nameTextDrawer.Initialize();
        changeBackground.Initialize();
        question.Initialize();

        //テキストを表示
        DisplayText();
    }

    private void DisplayText()
    {
        //前の行の名前欄や選択肢を非表示にしておく
        nameTextDrawer.DisableNameText();
        question.InitializeQuestionBranch();
        TalkDataShifter();
    }

    private void TalkDataShifter()
    {
        Content talkDataContent = talkData.Content[lineNumber];
        if (talkDataContent.Speaker != null)
        {
            nameTextDrawer.DisplayNameText(talkDataContent.Speaker);
        }
        if (talkDataContent.ChangeImage != null)
        {
            changeBackground.ChangeImages(talkDataContent.ChangeImage);
        }
        if (talkDataContent.QuestionData != null)
        {
            question.DisplayQuestion(talkDataContent.QuestionData);
        }
        if (talkDataContent.BGM != null)
        {
            SoundManager.Instance.ChangeBGM(talkDataContent.BGM);
        }
        if (talkDataContent.SE != null)
        {
            SoundManager.Instance.ChangeSE(talkDataContent.SE);
        }
        if (talkDataContent.Text != null)
        {
            mainTextDrawer.DisplayMainText(talkDataContent.Text);
            mainTextDrawer.DisplayTextRuby();
        }
    }

    private void ChangeLine(int increase)
    {
        unitTime = intervalTime;
        lineNumber += increase;
        mainTextDrawer.InitializeLine();
    }

    private void ChangeQuestionData()
    {
        QuestionData[] questionData = talkData.Content[lineNumber].QuestionData;
        if (questionData == null) 
            return;

        var nextFlag = questionData[question.GetCursorPlace()].NextFlag;
        if (nextFlag == null) 
            return;

        ChangeFlag(nextFlag);
    }

    private void ChangeFlag(KeyValuePair<string, bool>[] nextFlag)
    {
        foreach (KeyValuePair<string, bool> flags in nextFlag)
        {
            string flagName = flags.Key;
            bool flagValue = flags.Value;
            DebugLogger.Log(flagName+":"+ flagValue);
            if (flagValue)
            {
                FlagManager.Instance.AddFlag(flagName);
            }
            else
            {
                FlagManager.Instance.DeleteFlag(flagName);
            }
        }
    }
    
    public void ResetAction()
    {
        _onConversationStart = null;
        _onConversationEnd = null;
    }

    private void EndConversation()
    {
        QuestionData[] questionData = talkData.Content[lineNumber].QuestionData;
        string nextTalkData = null;
        if (questionData != null)
        {
            nextTalkData = questionData[question.GetCursorPlace()].NextTalkData;
        }

        initializeFlag = false;
        _onConversationEnd?.Invoke();
        if (nextTalkData != null){  //会話分岐
            InitializeFromJson(nextTalkData);
        }
        else
        {
            contentObject.SetActive(false);
        }
    }
}