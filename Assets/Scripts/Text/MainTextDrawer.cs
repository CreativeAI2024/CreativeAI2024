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
        //�ꕶ�����\�����邽�߁A�ŏ���0�����ɐݒ�
        _mainTextObject.maxVisibleCharacters = 0;
        _displayedSentenceLength = -1;
    }

    // �P�ʎ��� feedTime���Ƃɕ��͂��P�������\������
    public void Typewriter()
    {
        if (!AllowChangeLine())
        {
            //_displayedSentenceLength��maxVisibleCharacters�𐧌�B
            _displayedSentenceLength++;

            //�Q�ƘR��̖h�~
            if (_displayedSentenceLength > 0 && _mainTextObject.GetParsedText().Length > _displayedSentenceLength - 1)
            {
                //�O����e�L�X�g���ꕶ�������\������B
                _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
            }
        }
    }

    public void SkipTypewriter()
    {
        //�S�����\������Ă��Ȃ��ꍇ�ɃL�[���������Ƃ��A�^�O�Ȃ��̖{�����擾���A���̒�������
        //_displayedSentenceLength : �\������Ă���{���̂������ۂɉ�ʂɂ��邾���̒���
        //maxVisibleCharacters : TMP�̋@�\�ŕ\�����镶���̐��𐧌䂷��B�^�O�Ȃ��̖{���̒����������邱�ƂőS����\��
        _displayedSentenceLength = _mainTextObject.GetParsedText().Length;
        _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
        Debug.Log("LineSkipped");
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

    // ���̍s�́A���ׂĂ̕������\������Ă��Ȃ���΁A�s��ς��邱�Ƃ͂ł��Ȃ�
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

    // �e�L�X�g��\��
    public void DisplayTextRuby()
    {
        if (TryGetComponent(out TextMeshProRuby rb))
        {
            rb.Text = _mainTextObject.text;
        }
    }

    public void DisplayMainText(string[] words)
    {
        _mainTextObject.text = words[words.Length - 1];
    }

    //���̍s�֐i�ރA�C�R���̕\����\��
    public void NextLineIcon()
    {
        if (!AllowChangeLine())
        {
            //���̍s�֐i�ނ��Ƃ��ł��Ȃ��ꍇ�A���̍s�֐i�ރA�C�R�����\���ɂ���
            nextPageIcon.SetActive(false);
            animator.enabled = false;
        }
        else if (AllowChangeLine())
        {
            //���̍s�֐i�ނ��Ƃ��ł���ꍇ�A���̍s�֐i�ރA�C�R����\������
            if (_displayedSentenceLength > 0)
            {
                //�A�C�R���̈ʒu��ݒ�
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
        //���������̈ʒu���擾
        TMP_TextInfo textInfo = _mainTextObject.textInfo;
        string str = _mainTextObject.GetParsedText();
        if (string.IsNullOrEmpty(str)) return Vector2.zero;
        Vector2 characterVector = textInfo.characterInfo[str.Length - 1].bottomRight;
        if (str.EndsWith("��") || str.EndsWith("�c")) characterVector.y -= 20;
        Vector2 objectVector = _mainTextObject.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition;
        return characterVector + objectVector;
    }
}
