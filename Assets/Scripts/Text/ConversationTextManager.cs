using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ConversationTextManager : MonoBehaviour
{
    [SerializeField] MainTextDrawer mainTextDrawer;
    [SerializeField] NameTextDrawer nameTextDrawer;

    [SerializeField] TextAsset textAsset;
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
        // �P�ʎ��� feedTime���Ƃɕ��͂��P�������\������
        mainTextDrawer.Typewriter();

        // z�L�[�������ꂽ�Ƃ��A���̍s�ֈړ�
        ChangeLine();

        //���̍s�֐i�ރA�C�R���̕\����\��
        mainTextDrawer.GoToTheNextLineIcon();
    }

    public void Initiallize()
    {
        mainTextDrawer.Initiallize();

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

        mainTextDrawer.unitTime = 0f;

        //�ꕶ�����\�����邽�߁A�ŏ���0�����ɐݒ�
        mainTextDrawer.GetMainTextObject().maxVisibleCharacters = 0;
        //�e�L�X�g��\��
        MainText();
        mainTextDrawer.DisplayText();
    }


    public void ChangeLine()
    {
        if (_inputSetting.GetDecideKeyUp() || _inputSetting.GetCancelKeyUp())
        {
            DisplayText();
        }
    }

    void DisplayText()
    {
        //�S�����\������Ă���ꍇ�A���̍s�ֈړ�
        if (mainTextDrawer.CanGoToTheNextLine() && mainTextDrawer.unitTime > -0.45f)
        {
            if (_inputSetting.GetDecideKeyUp())
            {
                mainTextDrawer.GoToTheNextLine();
            }
            else if (_inputSetting.GetCancelKeyUp())
            {
                mainTextDrawer.GoToTheFormerLine();
            }
            MainText();
            mainTextDrawer.DisplayText();
        }
        else if (mainTextDrawer.unitTime > -0.45f)
        {
            //�S�����\������Ă��Ȃ��ꍇ�ɃL�[���������Ƃ��A�^�O�Ȃ��̖{�����擾���A���̒�������
            //mainTextDrawer._sentenceLength : �\������Ă���{���̂��Ƃ��Ƃ̒���
            //mainTextDrawer._displayedSentenceLength : �\������Ă���{���̂������ۂɉ�ʂɂ��邾���̒���
            //maxVisibleCharacters : TMP�̋@�\�ŕ\�����镶���̐��𐧌䂷��B�^�O�Ȃ��̖{���̒����������邱�ƂőS����\��
            mainTextDrawer.GetMainTextObject().maxVisibleCharacters = mainTextDrawer._displayedSentenceLength = mainTextDrawer._sentenceLength = mainTextDrawer.GetMainTextObject().GetParsedText().Length; 
            Debug.Log("LineSkipped");
        }
        else
        {
            //�G���[�΍�B�s�v���͂���B
            mainTextDrawer.unitTime = 0.2f;
        }
        if (mainTextDrawer.unitTime > -0.55f)//�A�ő΍�i�����X�N���[�����j
            mainTextDrawer.unitTime -= 0.35f;
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
                nameTextDrawer.NamePanelSwitch();
            }
            lineText = text;
        }
        mainTextDrawer.GetMainTextObject().text = lineText;
    }
}
