using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTextDrawer : MonoBehaviour
{
    private TextMeshProUGUI _mainTextObject;

    [SerializeField] Animator animator;
    [SerializeField] GameObject _nextPageIcon;

    [HideInInspector] private int _displayedSentenceLength = -1;

    [SerializeField] RectTransform iconObject;

    public void Initiallize()
    {
        _mainTextObject = GetComponent<TextMeshProUGUI>();
        //一文字ずつ表示するため、最初は0文字に設定
        SetMaxVisibleCharacters(0);
    }

    private void SetMaxVisibleCharacters(int num)
    {
        _mainTextObject.maxVisibleCharacters = num;
    }

    // 単位時間 feedTimeごとに文章を１文字ずつ表示する
    public void Typewriter()
    {
        if (!AllowChangeLine())
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
        if (!AllowChangeLine())
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
        //_displayedSentenceLength : 表示されている本文のうち実際に画面にあるだけの長さ
        //maxVisibleCharacters : TMPの機能で表示する文字の数を制御する。タグなしの本文の長さを代入することで全文を表示
        _displayedSentenceLength = _mainTextObject.GetParsedText().Length;
        SetMaxVisibleCharacters(_displayedSentenceLength);
        Debug.Log("LineSkipped");
    }

    //次の行へ進むアイコンの表示非表示
    public void NextLineIcon()
    {
        if (!AllowChangeLine())
        {
            //次の行へ進むことができない場合、次の行へ進むアイコンを非表示にする
            _nextPageIcon.SetActive(false);
            animator.enabled = false;
        }
        else if (AllowChangeLine())
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
            animator.enabled = true;
        }
    }

    // その行の、すべての文字が表示されていなければ、行を変えることはできない
    public bool AllowChangeLine()
    {
        string sentence = _mainTextObject.GetParsedText();
        return (_displayedSentenceLength > sentence.Length - 1);
    }

    public void InitializeLine()
    {
        _mainTextObject.maxVisibleCharacters = 0;
        _displayedSentenceLength = 0;
    }

    // テキストを表示
    public void DisplayTextRuby()
    {
        if (TryGetComponent(out TextMeshProRuby rb))
        {
            rb.Text = _mainTextObject.text;
        }
    }
    public void SplitMainText(string[] words)
    {
        _mainTextObject.text = words[words.Length - 1];
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
