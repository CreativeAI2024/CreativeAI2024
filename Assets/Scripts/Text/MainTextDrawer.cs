using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTextDrawer : MonoBehaviour
{
    private TextMeshProUGUI _mainTextObject;

    [SerializeField] Animator animator;
    [SerializeField] GameObject _nextPageIcon;

    [HideInInspector] public int _displayedSentenceLength = -1;
    [HideInInspector] public int _sentenceLength;

    [SerializeField] RectTransform iconObject;
    [HideInInspector] public List<string> _sentences = new();

    private int lineNumber = 0;

    public void Initiallize()
    {
        _mainTextObject = GetComponent<TextMeshProUGUI>();
    }

    public int GetLineNumber()
    {
        return lineNumber;
    }

    public void SetMaxVisibleCharacters(int num)
    {
        _mainTextObject.maxVisibleCharacters = num;
    }

    public void SetMainText(string text)
    {
        _mainTextObject.text = text;
    }

    public int GetParsedTextLength()
    {
        return _mainTextObject.GetParsedText().Length;
    }

    // 単位時間 feedTimeごとに文章を１文字ずつ表示する
    public void Typewriter()
    {
        if (!CanGoToTheNextLine())
        {
            //_displayedSentenceLengthでmaxVisibleCharactersを制御。
            _displayedSentenceLength++;

            //参照漏れの防止
            if (_displayedSentenceLength > 0 && _mainTextObject.GetParsedText().Length > _displayedSentenceLength - 1)
            {
                //前回よりテキストを一文字多く表示する。
                _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
            }
        }
    }

    public int GetDelayTime()
    {
        if (!CanGoToTheNextLine())
        {
            string sentence = _mainTextObject.GetParsedText();
            if (_displayedSentenceLength+1 > 0 && _mainTextObject.GetParsedText().Length > _displayedSentenceLength)
            {
                if (sentence[_displayedSentenceLength].Equals('。') || sentence[_displayedSentenceLength].Equals('！') || sentence[_displayedSentenceLength].Equals('？'))
                {
                    return 10;
                }
                else if (sentence[_displayedSentenceLength].Equals('、'))
                {
                    return 5;
                }
            }
        }
        return 1;
    }


    public void SkipTypewriter()
    {
        //全文が表示されていない場合にキーを押したとき、タグなしの本文を取得し、その長さを代入
        //_sentenceLength : 表示されている本文のもともとの長さ
        //_displayedSentenceLength : 表示されている本文のうち実際に画面にあるだけの長さ
        //maxVisibleCharacters : TMPの機能で表示する文字の数を制御する。タグなしの本文の長さを代入することで全文を表示
        _displayedSentenceLength = _sentenceLength = GetParsedTextLength();
        SetMaxVisibleCharacters(_displayedSentenceLength);
        Debug.Log("LineSkipped");
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
                Vector2 textPosition = LastTextPosition();
                if (textPosition == Vector2.zero) return;
                textPosition.x += 25f;

                iconObject.anchoredPosition = textPosition;
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
        return (_displayedSentenceLength > sentence.Length - 1);
    }

    //行の移動
    private void GoToLine(int increase)
    {
        if (0 <= lineNumber && lineNumber <= _sentences.Count - 1)
        {
            //次の行へ移動し、表示する文字数をリセット
            if (!((lineNumber <= 0 && increase == -1) || (lineNumber >= _sentences.Count - 1 && increase == 1)))
            {
                lineNumber += increase;
                _mainTextObject.maxVisibleCharacters = 0;
                _displayedSentenceLength = 0;
            }
        }
        else
        {
            Debug.Log("SceneEnded");
        }
    }

    // 次の行へ移動
    public void GoToTheNextLine()
    {
        GoToLine(1);

        Debug.Log("NextLine");
    }
    // 前の行へ移動
    public void GoToTheFormerLine()
    {
        GoToLine(-1);
        Debug.Log("FormerLine");
    }

    // テキストを表示
    public void DisplayTextRuby()
    {
        if (TryGetComponent(out TextMeshProRuby rb))
        {
            rb.Text = _mainTextObject.text;
        }
        //テキストを取得し、表示。
        //Debug.Log(_mainTextObject.text);
    }

    private Vector2 LastTextPosition()
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
