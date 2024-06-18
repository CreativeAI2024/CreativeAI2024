using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;

public class ConversationTextManager : MonoBehaviour
{
    [SerializeField] private MainTextDrawer mainTextDrawer;
    [SerializeField] private NameTextDrawer nameTextDrawer;
    [SerializeField] private TextAsset textAsset;

    [SerializeField] private float intervalTime;
    private float unitTime;
    private InputSetting _inputSetting;

    private List<string> _sentences = new();
    private int lineNumber = 0;

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
                    ChangeLine(1);
                    Debug.Log("NextLine");
                }
                else if (_inputSetting.GetCancelKeyUp() && 0 < lineNumber)
                {
                    ChangeLine(-1);
                    Debug.Log("BackLine");
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

        DisplayText();

        //次の行へ進むアイコンの表示非表示
        mainTextDrawer.NextLineIcon();
    }

    private void Initiallize()
    {
        mainTextDrawer.Initiallize();
        
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
            Debug.LogError("テキストファイルが見つかりませんでした");
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
        //名前がある場合、名前を表示。名前がない場合、名前表示を非表示にする。名前は「名前:文章」という形式で記述する。
        mainTextDrawer.DisplayMainText(words);
        mainTextDrawer.DisplayTextRuby();
        if (nameTextDrawer != null)
        {
            nameTextDrawer.DisplayNameText(words);
        }
    }

    private void ChangeLine(int increase)
    {
        unitTime = intervalTime;
        lineNumber += increase;
        mainTextDrawer.InitializeLine();
    }
}