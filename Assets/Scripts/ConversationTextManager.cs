using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor.SceneManagement;

public class ConversationTextManager : MonoBehaviour
{
    public MainTextDrawer mainTextDrawer;
    public NameTextDrawer nameTextDrawer;
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

        // �P�ʎ��� feedTime���Ƃɕ��͂��P�������\������
        mainTextDrawer.Typewriter();

        // z�L�[�������ꂽ�Ƃ��A���̍s�ֈړ�
        ChangeLine();

        //���̍s�֐i�ރA�C�R���̕\����\��
        mainTextDrawer.GoToTheNextLineIcon();
    }

    public void Initiallize()
    {
        mainTextDrawer.SetLineNumber(lineNumber);

        //�e�L�X�g�t�@�C���̓ǂݍ��݁B_sentences�Ɋi�[
        if (textAsset == null)
        {
            Debug.LogError("�e�L�X�g�t�@�C����������܂���ł���");
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

        //�ꕶ�����\�����邽�߁A�ŏ���0�����ɐݒ�
        mainTextDrawer._mainTextObject.maxVisibleCharacters = 0;
        //�e�L�X�g��\��
        MainText();
        mainTextDrawer.DisplayText();
    }


    public void ChangeLine()
    {
        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.X))
        {
            DisplayText();
        }
    }

    void DisplayText()
    {
        //�S�����\������Ă���ꍇ�A���̍s�ֈړ�
        if (mainTextDrawer.CanGoToTheNextLine() && _time > -0.45f)
        {
            if (Input.GetKeyUp(KeyCode.Z))
            {
                mainTextDrawer.GoToTheNextLine();
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                mainTextDrawer.GoToTheFormerLine();
            }
            MainText();
            mainTextDrawer.DisplayText();
        }
        else if (_time > -0.45f)
        {
            //�S�����\������Ă��Ȃ��ꍇ�ɃL�[���������Ƃ��A�^�O�Ȃ��̖{�����擾���A���̒�������
            //mainTextDrawer._sentenceLength : �\������Ă���{���̂��Ƃ��Ƃ̒���
            //mainTextDrawer._displayedSentenceLength : �\������Ă���{���̂������ۂɉ�ʂɂ��邾���̒���
            //maxVisibleCharacters : TMP�̋@�\�ŕ\�����镶���̐��𐧌䂷��B�^�O�Ȃ��̖{���̒����������邱�ƂőS����\��
            mainTextDrawer._mainTextObject.maxVisibleCharacters = mainTextDrawer._displayedSentenceLength = mainTextDrawer._sentenceLength = mainTextDrawer._mainTextObject.GetParsedText().Length; 
            Debug.Log("LineSkipped");
        }
        else
        {
            //�G���[�΍�B�s�v���͂���B
            _time = 0.2f;
        }
        if (_time > -0.55f)//�A�ő΍�i�����X�N���[�����j
            _time -= 0.35f;
    }

    public string GetCurrentSentence()
    {
        //���݂̍s���擾
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
        //���O������ꍇ�A���O��\���B���O���Ȃ��ꍇ�A���O�\�����\���ɂ���B���O�́u���O:���́v�Ƃ����`���ŋL�q����B
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
                nameTextDrawer._nameTextPrefab.SetActive(false);
            }
            lineText = text;
        }
        mainTextDrawer._mainTextObject.text = lineText;
    }
}
