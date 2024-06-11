using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainTextDrawer : MonoBehaviour
{
    public TextMeshProUGUI _mainTextObject;
    public GameObject _mainTextPrefab;

    private RubyDrawer rb;

    public Animator animator;
    public GameObject _nextPageIcon;
    public float _feedTime;

    public float _time;
    public int _displayedSentenceLength = -1;
    public int _sentenceLength;

    
    [SerializeField] Image iconObject;
    public List<string> _sentences = new List<string>();

    private int lineNumber;



    public void SetLineNumber(int settingLineNumber)
    {
        lineNumber = settingLineNumber;
    }

    public int GetLineNumber()
    {
        return lineNumber;
    }

    // 単位時間 feedTimeごとに文章を１文字ずつ表示する
    public void Typewriter()
    {
        _time += Time.deltaTime;
        if (_time >= _feedTime)
        {
            _time -= _feedTime;

            if (!CanGoToTheNextLine())
            {
                string sentence = _mainTextObject.GetParsedText();

                //_displayedSentenceLengthでmaxVisibleCharactersを制御。今回の場合、ここは直接変更にしてもいいかもしれない。
                _displayedSentenceLength++;

                //参照漏れの防止
                if (_displayedSentenceLength > 0 && _mainTextObject.GetParsedText().Length > _displayedSentenceLength - 1)
                {
                    //前回よりテキストを一文字多く表示する。
                    _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
                    //テキスト音を鳴らす
                    if (_displayedSentenceLength % 3 == 0)
                    {
                        //テキスト音を鳴らすためのコード
                        //GameManager.Instance.audioManager.CharSE();
                    }
                    if (sentence[_displayedSentenceLength - 1].Equals('。') || sentence[_displayedSentenceLength - 1].Equals('！') || sentence[_displayedSentenceLength - 1].Equals('？'))
                    {
                        //、と。で表示速度を変える。
                        _time -= _feedTime*10;
                    }
                    else if (sentence[_displayedSentenceLength - 1].Equals('、'))
                    {
                        _time -= _feedTime*5;
                    }
                }
            }
        }
    }

    //次の行へ進むアイコンの表示非表示
    public void GoToTheNextLineIcon()
    {
        if (!CanGoToTheNextLine())
        {
            //次の行へ進むことができない場合、次の行へ進むアイコンを非表示にする
            _nextPageIcon.SetActive(false);
            if (animator.enabled == true)
            {
                animator.enabled = false;
            }
        }
        else if (CanGoToTheNextLine())
        {
            //次の行へ進むことができる場合、次の行へ進むアイコンを表示する
            if (_displayedSentenceLength > 0)
            {
                //アイコンの位置を設定
                Vector2 textPosition = lastTextPosition();
                if (textPosition == Vector2.zero) return;
                textPosition.x += 25f;
                RectTransform iconTransform = iconObject.GetComponent<RectTransform>();

                //Debug.Log("YouCanGoToTheNextLine");
                iconTransform.anchoredPosition = textPosition;
            }
            _nextPageIcon.SetActive(true);
            if (animator.enabled == false)
            {
                animator.enabled = true;
            }
        }
    }

    // その行の、すべての文字が表示されていなければ、まだ次の行へ進むことはできない
    public bool CanGoToTheNextLine()
    {
        string sentence = _mainTextObject.GetParsedText();
        _sentenceLength = sentence.Length;
        return (_displayedSentenceLength > sentence.Length - 1);
    }

    // 次の行へ移動
    public void GoToTheNextLine()
    {
        _time = 0.04f;
        if (lineNumber < _sentences.Count - 1)
        {
            //次の行へ移動し、表示する文字数をリセット
            lineNumber++;
            _mainTextObject.maxVisibleCharacters = 0;
            _displayedSentenceLength = 0;

            Debug.Log("GoNextLine");
        }
        else
        {
            Debug.Log("SceneEnded");
        }
    }
    // 前の行へ移動
    public void GoToTheFormerLine()
    {
        _time = 0.04f;
        if (lineNumber > 0)
        {
            //前の行へ移動し、表示する文字数をリセット
            lineNumber--;
            _mainTextObject.maxVisibleCharacters = 0;
            _displayedSentenceLength = 0;

            Debug.Log("GoFormerLine");
        }
        else
        {
            Debug.Log("TheFirstLine!");
        }
    }

    // テキストを表示
    public void DisplayText()
    {
     
        if (TryGetComponent(out RubyDrawer rb))
        {
            rb = this.GetComponent<RubyDrawer>();
            rb.RubySpawner(_mainTextObject.text);
        }
        //テキストを取得し、表示。

        //_mainTextObject.text = MainText();
        Debug.Log(_mainTextObject.text);
    }

    public Vector2 lastTextPosition()
    {
        //末尾文字の位置を取得
        TMP_TextInfo textInfo = _mainTextObject.textInfo;
        string str = _mainTextObject.GetParsedText();
        if (str == "") return new Vector2(0, 0);
        Vector2 character_vector = textInfo.characterInfo[str.Length - 1].bottomRight;
        if (str.EndsWith("─") || str.EndsWith("…")) character_vector.y -= 20;
        Vector2 object_vector = _mainTextObject.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition;
        return character_vector + object_vector;
    }
}
