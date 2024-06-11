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

    // �P�ʎ��� feedTime���Ƃɕ��͂��P�������\������
    public void Typewriter()
    {
        _time += Time.deltaTime;
        if (_time >= _feedTime)
        {
            _time -= _feedTime;

            if (!CanGoToTheNextLine())
            {
                string sentence = _mainTextObject.GetParsedText();

                //_displayedSentenceLength��maxVisibleCharacters�𐧌�B����̏ꍇ�A�����͒��ڕύX�ɂ��Ă�������������Ȃ��B
                _displayedSentenceLength++;

                //�Q�ƘR��̖h�~
                if (_displayedSentenceLength > 0 && _mainTextObject.GetParsedText().Length > _displayedSentenceLength - 1)
                {
                    //�O����e�L�X�g���ꕶ�������\������B
                    _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
                    //�e�L�X�g����炷
                    if (_displayedSentenceLength % 3 == 0)
                    {
                        //�e�L�X�g����炷���߂̃R�[�h
                        //GameManager.Instance.audioManager.CharSE();
                    }
                    if (sentence[_displayedSentenceLength - 1].Equals('�B') || sentence[_displayedSentenceLength - 1].Equals('�I') || sentence[_displayedSentenceLength - 1].Equals('�H'))
                    {
                        //�A�ƁB�ŕ\�����x��ς���B
                        _time -= _feedTime*10;
                    }
                    else if (sentence[_displayedSentenceLength - 1].Equals('�A'))
                    {
                        _time -= _feedTime*5;
                    }
                }
            }
        }
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

    // ���̍s�́A���ׂĂ̕������\������Ă��Ȃ���΁A�܂����̍s�֐i�ނ��Ƃ͂ł��Ȃ�
    public bool CanGoToTheNextLine()
    {
        string sentence = _mainTextObject.GetParsedText();
        _sentenceLength = sentence.Length;
        return (_displayedSentenceLength > sentence.Length - 1);
    }

    // ���̍s�ֈړ�
    public void GoToTheNextLine()
    {
        _time = 0.04f;
        if (lineNumber < _sentences.Count - 1)
        {
            //���̍s�ֈړ����A�\�����镶���������Z�b�g
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
    // �O�̍s�ֈړ�
    public void GoToTheFormerLine()
    {
        _time = 0.04f;
        if (lineNumber > 0)
        {
            //�O�̍s�ֈړ����A�\�����镶���������Z�b�g
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

    // �e�L�X�g��\��
    public void DisplayText()
    {
     
        if (TryGetComponent(out RubyDrawer rb))
        {
            rb = this.GetComponent<RubyDrawer>();
            rb.RubySpawner(_mainTextObject.text);
        }
        //�e�L�X�g���擾���A�\���B

        //_mainTextObject.text = MainText();
        Debug.Log(_mainTextObject.text);
    }

    public Vector2 lastTextPosition()
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
