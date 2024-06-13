using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ConversationTextManager : MonoBehaviour
{
    [SerializeField] MainTextDrawer mainTextDrawer;
    [SerializeField] NameTextDrawer nameTextDrawer;

    [SerializeField] TextAsset textAsset;
    private InputSetting _inputSetting;

    // Start is called before the first frame update
    void Start()
    {
        _inputSetting = InputSetting.Load();
        Initiallize();
    }

    // Update is called once per frame
    void Update()
    {
        // 単位時間 feedTimeごとに文章を１文字ずつ表示する
        mainTextDrawer.Typewriter();

        // zキーが離されたとき、次の行へ移動
        ChangeLine();

        //次の行へ進むアイコンの表示非表示
        mainTextDrawer.GoToTheNextLineIcon();
    }

    public void Initiallize()
    {
        mainTextDrawer.Initiallize();

        //テキストファイルの読み込み。_sentencesに格納
        if (textAsset == null)
        {
            Debug.LogError("テキストファイルが見つかりませんでした");
            return;
        }
        StringReader reader = new StringReader(textAsset.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (line == "") continue;
            mainTextDrawer._sentences.Add(line);
        }

        mainTextDrawer.unitTime = 0f;

        //一文字ずつ表示するため、最初は0文字に設定
        mainTextDrawer.GetMainTextObject().maxVisibleCharacters = 0;
        //テキストを表示
        MainText();
        mainTextDrawer.DisplayText();
    }


    public void ChangeLine()
    {
        if (_inputSetting.GetDecideKeyUp() || _inputSetting.GetCancelKeyUp())
        {
            DisplayText();
        }
    }

    void DisplayText()
    {
        //全文が表示されている場合、次の行へ移動
        if (mainTextDrawer.CanGoToTheNextLine() && mainTextDrawer.unitTime > -0.45f)
        {
            if (_inputSetting.GetDecideKeyUp())
            {
                mainTextDrawer.GoToTheNextLine();
            }
            else if (_inputSetting.GetCancelKeyUp())
            {
                mainTextDrawer.GoToTheFormerLine();
            }
            MainText();
            mainTextDrawer.DisplayText();
        }
        else if (mainTextDrawer.unitTime > -0.45f)
        {
            //全文が表示されていない場合にキーを押したとき、タグなしの本文を取得し、その長さを代入
            //mainTextDrawer._sentenceLength : 表示されている本文のもともとの長さ
            //mainTextDrawer._displayedSentenceLength : 表示されている本文のうち実際に画面にあるだけの長さ
            //maxVisibleCharacters : TMPの機能で表示する文字の数を制御する。タグなしの本文の長さを代入することで全文を表示
            mainTextDrawer.GetMainTextObject().maxVisibleCharacters = mainTextDrawer._displayedSentenceLength = mainTextDrawer._sentenceLength = mainTextDrawer.GetMainTextObject().GetParsedText().Length; 
            Debug.Log("LineSkipped");
        }
        else
        {
            //エラー対策。不要説はある。
            mainTextDrawer.unitTime = 0.2f;
        }
        if (mainTextDrawer.unitTime > -0.55f)//連打対策（爆速スクロール等）
            mainTextDrawer.unitTime -= 0.35f;
    }

    public string GetCurrentSentence()
    {
        //現在の行を取得
        if (mainTextDrawer.GetLineNumber() >= mainTextDrawer._sentences.Count)
        {
            return null;
        }
        return mainTextDrawer._sentences[mainTextDrawer.GetLineNumber()];
    }

    public void MainText()
    {
        string text = GetCurrentSentence();
        string[] words = text.Split(':');
        //名前がある場合、名前を表示。名前がない場合、名前表示を非表示にする。名前は「名前:文章」という形式で記述する。
        string lineText;
        if (words.Length > 1)
        {
            if (!(nameTextDrawer == null)) { 
                nameTextDrawer.NameText(words[0]);
            }
            lineText = words[1];
        }
        else
        {
            if (!(nameTextDrawer == null))
            {
                nameTextDrawer.NamePanelSwitch();
            }
            lineText = text;
        }
        mainTextDrawer.GetMainTextObject().text = lineText;
    }
}
