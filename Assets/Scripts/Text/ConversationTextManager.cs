using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConversationTextManager : MonoBehaviour
{
    [SerializeField] private MainTextDrawer mainTextDrawer;
    [SerializeField] private NameTextDrawer nameTextDrawer;
    [SerializeField] private TextAsset textAsset;

    [SerializeField] private float intervalTime;
    private float unitTime;
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
        unitTime += Time.deltaTime;
        TimeKeeper();

        // zキーが離されたとき、次の行へ移動
        ChangeLine();

        //次の行へ進むアイコンの表示非表示
        mainTextDrawer.GoToTheNextLineIcon();
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
            mainTextDrawer._sentences.Add(line);
        }

        unitTime = 0f;

        //一文字ずつ表示するため、最初は0文字に設定
        mainTextDrawer.SetMaxVisibleCharacters(0);
        //テキストを表示
        SplitText();
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
        if (mainTextDrawer.GetLineNumber() >= mainTextDrawer._sentences.Count)
        {
            return null;
        }
        return mainTextDrawer._sentences[mainTextDrawer.GetLineNumber()];
    }

    private void ChangeLine()
    {
        if (_inputSetting.GetDecideKeyUp() || _inputSetting.GetCancelKeyUp())
        {
            DisplayText();
        }
        SplitText();
        mainTextDrawer.DisplayTextRuby();
    }

    private void DisplayText()
    {
        if (mainTextDrawer.CanGoToTheNextLine() && unitTime > -0.45f)
        {
            if (_inputSetting.GetDecideKeyUp())
            {
                unitTime = intervalTime;
                mainTextDrawer.GoToTheNextLine();
            }
            else if (_inputSetting.GetCancelKeyUp())
            {
                unitTime = intervalTime;
                mainTextDrawer.GoToTheFormerLine();
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

    private void SplitText()
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
        mainTextDrawer.SetMainText(lineText);
    }
}
