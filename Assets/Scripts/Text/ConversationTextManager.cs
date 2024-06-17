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
        TimeKeeper();

        // zキーが離されたとき、次の行へ移動
        ChangeText();

        //次の行へ進むアイコンの表示非表示
        mainTextDrawer.NextLineIcon();
    }

    private void Initiallize()
    {
        mainTextDrawer.Initiallize();

        //テキストファイルの読み込み。_sentencesに格納
        if (textAsset == null)
        {
            Debug.LogError("テキストファイルが見つかりませんでした");
            return;
        }
        StringReader reader = new(textAsset.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (line == "") continue;
            _sentences.Add(line);
        }

        unitTime = 0f;

        //テキストを表示
        string[] words = SplitText();
        mainTextDrawer.SplitMainText(words);
        if (!(nameTextDrawer == null))
        {
            nameTextDrawer.SplitNameText(words);
        }
        mainTextDrawer.DisplayTextRuby();
    }

    private void TimeKeeper()
    {
        if (unitTime >= intervalTime)
        {
            unitTime -= intervalTime * mainTextDrawer.GetDelayTime();
            mainTextDrawer.Typewriter();
        }
    }

    private string GetCurrentSentence()
    {
        //現在の行を取得
        if (lineNumber >= _sentences.Count)
        {
            return null;
        }
        return _sentences[lineNumber];
    }

    private void ChangeText()
    {
        if (_inputSetting.GetDecideKeyUp() || _inputSetting.GetCancelKeyUp())
        {
            DisplayText();
        }
        string[] words = SplitText();
        mainTextDrawer.SplitMainText(words);
        if (!(nameTextDrawer == null))
        {
            nameTextDrawer.SplitNameText(words);
        }
        mainTextDrawer.DisplayTextRuby();
    }

    private void DisplayText()
    {
        if (mainTextDrawer.AllowChangeLine() && unitTime > -0.45f)
        {
            if (0 <= lineNumber && lineNumber <= _sentences.Count - 1)
            {
                //次の行へ移動し、表示する文字数をリセット
                if (_inputSetting.GetDecideKeyUp() && !(lineNumber >= _sentences.Count - 1))
                {
                    ChangeLine(1);
                    Debug.Log("NextLine");
                }
                else if (_inputSetting.GetCancelKeyUp() && !((lineNumber <= 0)))
                {
                    ChangeLine(-1);
                    Debug.Log("BackLine");
                }
            }
            else
            {
                Debug.Log("SceneEnded");
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

    private void ChangeLine(int increase)
    {
        unitTime = intervalTime;
        lineNumber += increase;
        mainTextDrawer.InitializeLine();
    }

    private string[] SplitText()
    {
        string text = GetCurrentSentence();
        string[] words = text.Split(':');
        //名前がある場合、名前を表示。名前がない場合、名前表示を非表示にする。名前は「名前:文章」という形式で記述する。
        return words;
    }
}