using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConversationTextManager : MonoBehaviour
{
    [SerializeField] private MainTextDrawer mainTextDrawer;
    [SerializeField] private NameTextDrawer nameTextDrawer;
    [SerializeField] private ChangeBackground changeBackground;
    [SerializeField] private Question question;
    [SerializeField] private TextAsset textAsset;
    [SerializeField] private Pause pause;
    [SerializeField] private float intervalTime;
    private float unitTime;
    private InputSetting _inputSetting;

    private List<string> _sentences = new();
    private int lineNumber;


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
                if (_inputSetting.GetDecideKeyUp() && lineNumber < _sentences.Count - 1)
                {
                    question.QuestionOutput();  //仮の出力
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
                    question.QuestionOutput();  //仮の出力
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
        _sentences.Clear();
        unitTime = 0f;

        //テキストファイルの読み込み。_sentencesに格納
        LoadTextFile();

        //テキストを表示
        DisplayText();
    }

    private void LoadTextFile()
    {
        if (textAsset == null)
        {
            DebugLogger.Log("テキストファイルが見つかりませんでした");
            return;
        }
        using StringReader reader = new(textAsset.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (string.IsNullOrEmpty(line)) continue;
            _sentences.Add(line);
        }
    }

    private void DisplayText()
    {
        //現在の行を取得
        string text = _sentences[lineNumber];
        string[] words = text.Split(':');
        //前の行の名前欄や選択肢を非表示にしておく
        nameTextDrawer.DisableNameText();
        question.InitializeQuestionBranch();
        TextTagShifter(words);
    }

    private void TextTagShifter(string[] words)
    {
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].StartsWith("[speaker]"))  //[speaker]タグを探す
            {
                nameTextDrawer.DisplayNameText(words[i]);
            }
            else if (words[i].StartsWith("[image]"))  //[image]タグを探す
            {
                changeBackground.ChangeImages(words[i]);
            }
            else if (words[i].StartsWith("[question]"))  //[question]タグを探す
            {
                question.DisplayQuestion(words[i]);
            }
            else
            {
                mainTextDrawer.DisplayMainText(words[i]);
                mainTextDrawer.DisplayTextRuby();
            }
        }
    }

    private void ChangeLine(int increase)
    {
        unitTime = intervalTime;
        lineNumber += increase;
        mainTextDrawer.InitializeLine();
    }
}