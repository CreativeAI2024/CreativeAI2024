using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MainTextDrawer : MonoBehaviour
{
    private TextMeshProUGUI _mainTextObject;

    [SerializeField] Animator animator;
    [SerializeField] GameObject nextPageIcon;

    private int _displayedSentenceLength;

    [SerializeField] RectTransform iconObject;

    public void Initialize()
    {
        _mainTextObject = GetComponent<TextMeshProUGUI>();
        _mainTextObject.maxVisibleCharacters = 0;
        _displayedSentenceLength = -1;
    }

    public void Typewriter()
    {
        if (!AllowChangeLine())
        {
            _displayedSentenceLength++;

            if (_displayedSentenceLength > 0 && _mainTextObject.GetParsedText().Length > _displayedSentenceLength - 1)
            {
                _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
            }
        }
    }

    public void SkipTypewriter()
    {
        _displayedSentenceLength = _mainTextObject.GetParsedText().Length;
        _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
        DebugLogger.Log("LineSkipped");
    }

    public int GetDelayTime()
    {
        if (!AllowChangeLine())
        {
            string sentence = _mainTextObject.GetParsedText();
            if (_displayedSentenceLength+1 > 0 && _mainTextObject.GetParsedText().Length > _displayedSentenceLength)
            {
                // 文字コードいじったからエラー出るかも
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

    public bool AllowChangeLine()
    {
        string sentence = _mainTextObject.GetParsedText();
        return _displayedSentenceLength > sentence.Length - 1;
    }

    public void InitializeLine()
    {
        _mainTextObject.maxVisibleCharacters = 0;
        _displayedSentenceLength = 0;
    }
    
    public void DisplayTextRuby()
    {
        if (TryGetComponent(out TextMeshProRuby rb))
        {
            rb.Text = _mainTextObject.text;
        }
    }

    public void DisplayMainText(string word)
    {
        _mainTextObject.text = word;
    }

    public void NextLineIcon()
    {
        if (!AllowChangeLine())
        {
            nextPageIcon.SetActive(false);
            animator.enabled = false;
        }
        else if (AllowChangeLine())
        {
            if (_displayedSentenceLength > 0)
            {
                Vector2 textPosition = LastTextPosition();
                if (textPosition == Vector2.zero) return;
                textPosition.x += 25f;

                iconObject.anchoredPosition = textPosition;
            }
            nextPageIcon.SetActive(true);
            animator.enabled = true;
        }
    }

    private Vector2 LastTextPosition()
    {
        TMP_TextInfo textInfo = _mainTextObject.textInfo;
        string str = _mainTextObject.GetParsedText();
        if (string.IsNullOrEmpty(str)) return Vector2.zero;
        Vector2 characterVector = textInfo.characterInfo[str.Length - 1].bottomRight;
        if (str.EndsWith("─") || str.EndsWith("…")) characterVector.y -= 20;
        Vector2 objectVector = _mainTextObject.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition;
        return characterVector + objectVector;
    }
}
