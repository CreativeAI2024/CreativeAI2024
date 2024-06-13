using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainTextDrawer : MonoBehaviour
{
    private TextMeshProUGUI _mainTextObject;

    [SerializeField] Animator animator;
    [SerializeField] GameObject _nextPageIcon;
    [SerializeField] float intervalTime;

    [HideInInspector] public float unitTime;
    [HideInInspector] public int _displayedSentenceLength = -1;
    [HideInInspector] public int _sentenceLength;

    [SerializeField] RectTransform iconObject;
    [HideInInspector] public List<string> _sentences = new List<string>();

    private int lineNumber = 0;

    public void Initiallize()
    {
        _mainTextObject = GetComponent<TextMeshProUGUI>();
    }

    public int GetLineNumber()
    {
        return lineNumber;
    }

    public TextMeshProUGUI GetMainTextObject()
    {
        return this._mainTextObject;
    }

    // �P�ʎ��� feedTime���Ƃɕ��͂��P�������\������
    public void Typewriter()
    {
        unitTime += Time.deltaTime;
        if (unitTime >= intervalTime)
        {
            unitTime -= intervalTime;

            if (!CanGoToTheNextLine())
            {
                string sentence = _mainTextObject.GetParsedText();

                //_displayedSentenceLength��maxVisibleCharacters�𐧌�B
                _displayedSentenceLength++;

                //�Q�ƘR��̖h�~
                if (_displayedSentenceLength > 0 && _mainTextObject.GetParsedText().Length > _displayedSentenceLength - 1)
                {
                    //�O����e�L�X�g���ꕶ�������\������B
                    _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;

                    if (sentence[_displayedSentenceLength - 1].Equals('�B') || sentence[_displayedSentenceLength - 1].Equals('�I') || sentence[_displayedSentenceLength - 1].Equals('�H'))
                    {
                        //�A�ƁB�ŕ\�����x��ς���B
                        PauseTypewriter(10);
                    }
                    else if (sentence[_displayedSentenceLength - 1].Equals('�A'))
                    {
                        PauseTypewriter(5);
                    }
                }
            }
        }
    }

    private void PauseTypewriter(int multiple)
    {
        unitTime -= intervalTime * multiple;
    }

    //���̍s�֐i�ރA�C�R���̕\����\��
    public void GoToTheNextLineIcon()
    {
        if (!CanGoToTheNextLine())
        {
            //���̍s�֐i�ނ��Ƃ��ł��Ȃ��ꍇ�A���̍s�֐i�ރA�C�R�����\���ɂ���
            _nextPageIcon.SetActive(false);
            if (animator.enabled == true)
            {
                animator.enabled = false;
            }
        }
        else if (CanGoToTheNextLine())
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
            _nextPageIcon.SetActive(true);
            if (animator.enabled == false)
            {
                animator.enabled = true;
            }
        }
    }

    // ���̍s�́A���ׂĂ̕������\������Ă��Ȃ���΁A�܂����̍s�֐i�ނ��Ƃ͂ł��Ȃ�
    public bool CanGoToTheNextLine()
    {
        string sentence = _mainTextObject.GetParsedText();
        return (_displayedSentenceLength > sentence.Length - 1);
    }

    //�s�̈ړ�
    private void GoToLine(int increase)
    {
        unitTime = intervalTime;
        if (0 <= lineNumber && lineNumber <= _sentences.Count - 1)
        {
            //���̍s�ֈړ����A�\�����镶���������Z�b�g
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

    // ���̍s�ֈړ�
    public void GoToTheNextLine()
    {
        GoToLine(1);
        Debug.Log("NextLine");
    }
    // �O�̍s�ֈړ�
    public void GoToTheFormerLine()
    {
        GoToLine(-1);
        Debug.Log("FormerLine");
    }

    // �e�L�X�g��\��
    public void DisplayText()
    {
        if (TryGetComponent(out TextMeshProRuby rb))
        {
            rb.Text = _mainTextObject.text;
        }
        //�e�L�X�g���擾���A�\���B
        Debug.Log(_mainTextObject.text);
    }

    private Vector2 LastTextPosition()
    {
        //���������̈ʒu���擾
        TMP_TextInfo textInfo = _mainTextObject.textInfo;
        string str = _mainTextObject.GetParsedText();
        if (str == "") return new Vector2(0, 0);
        Vector2 character_vector = textInfo.characterInfo[str.Length - 1].bottomRight;
        if (str.EndsWith("��") || str.EndsWith("�c")) character_vector.y -= 20;
        Vector2 object_vector = _mainTextObject.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition;
        return character_vector + object_vector;
    }
}
