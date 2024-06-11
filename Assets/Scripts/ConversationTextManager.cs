using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConversationTextManager : MonoBehaviour
{
    public static ConversationTextManager Instance { get; private set; }

    public MainTextDrawer  mainTextDrawer;
    public NameTextDrawer  nameTextDrawer;
    private float _time;
    public TextAsset textAsset;
    private int lineNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        Initiallize();
    }

    // Update is called once per frame
    void Update()
    {
        
        _time = mainTextDrawer._time;

        // 単位時間 feedTimeごとに文章を１文字ずつ表示する
        mainTextDrawer.Typewriter();

        // zキーが離されたとき、次の行へ移動
        ChangeLine(KeyCode.Z, KeyCode.X);

        //次の行へ進むアイコンの表示非表示
        mainTextDrawer.GoToTheNextLineIcon();
    }

    public void Initiallize()
    {
        Instance = this;
        //下記三行はあまり意味がない。
        /*_mainTextObject = _mainTextObject.GetComponent<TextMeshProUGUI>();
        _nameTextObject = _nameTextObject.GetComponent<TextMeshProUGUI>();
        rb = _mainTextObject.GetComponent<TextMeshProRuby>();*/

        mainTextDrawer = mainTextDrawer.GetComponent<MainTextDrawer>();
        nameTextDrawer = nameTextDrawer.GetComponent<NameTextDrawer>();

        mainTextDrawer.SetLineNumber(lineNumber);


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

        _time = 0f;

        //一文字ずつ表示するため、最初は0文字に設定
        mainTextDrawer._mainTextObject.maxVisibleCharacters = 0;
        //テキストを表示
        MainText();
        mainTextDrawer.DisplayText();

    }


    public void ChangeLine(KeyCode keyNext, KeyCode keyFormer)
    {

        if (Input.GetKeyUp(keyNext))
        {
            //全文が表示されている場合、次の行へ移動
            if (mainTextDrawer.CanGoToTheNextLine() && _time > -0.45f)
            {
                mainTextDrawer.GoToTheNextLine();
                MainText();
                mainTextDrawer.DisplayText();

            }
            else if (_time > -0.45f)
            {
                //全文が表示されていない場合にキーを押したとき、全文を表示
                mainTextDrawer._mainTextObject.maxVisibleCharacters = mainTextDrawer._displayedSentenceLength = mainTextDrawer._sentenceLength;
                Debug.Log("LineSkipped");
            }
            else
            {
                //エラー対策。不要説はある。
                _time = 0.2f;
            }
            if (_time > -0.55f)//連打対策（爆速スクロール等）
                _time -= 0.35f;

        }
        else if (Input.GetKeyUp(keyFormer))
        {
            if (mainTextDrawer.CanGoToTheNextLine() && _time > -0.45f)
            {
                //GameManager.Instance.audioManager.PageSE();
                mainTextDrawer.GoToTheFormerLine();
                MainText();
                mainTextDrawer.DisplayText();

            }
            else if (_time > -0.45f)
            {
                //全文が表示されていない場合にキーを押したとき、全文を表示
                mainTextDrawer._mainTextObject.maxVisibleCharacters = mainTextDrawer._displayedSentenceLength = mainTextDrawer._sentenceLength;
                Debug.Log("LineSkipped");
            }
            else
            {
                //エラー対策。不要説はある。
                _time = 0.2f;
            }
            if (_time > -0.55f)//連打対策（爆速スクロール等）
                _time -= 0.35f;

        }

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
        string[] words = text.Split(":");
        //名前がある場合、名前を表示。名前がない場合、名前表示を非表示にする。名前は「名前:文章」という形式で記述する。
        if (words.Length > 1)
        {
            nameTextDrawer.NameText(words[0]);
            mainTextDrawer._mainTextObject.text = words[1];
        }
        else
        {
            nameTextDrawer._nameTextPrefab.SetActive(false);
            mainTextDrawer._mainTextObject.text = text;
        }
    }
}
