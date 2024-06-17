using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;

public class ConversationTextManager : MonoBehaviour
{
    [SerializeField] private MainTextDrawer mainTextDrawer;
    [SerializeField] private NameTextDrawer nameTextDrawer;
    [SerializeField] private TextAsset textAsset;

    [SerializeField] private float intervalTime;
    private float unitTime;
    private InputSetting _inputSetting;

    private List<string> _sentences = new();
    private int lineNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        _inputSetting = InputSetting.Load();
        Initiallize();
    }

    // Update is called once per frame
    void Update()
    {
        unitTime += Time.deltaTime;
        TimeKeeper();

        // z�L�[�������ꂽ�Ƃ��A���̍s�ֈړ�
        ChangeText();

        //���̍s�֐i�ރA�C�R���̕\����\��
        mainTextDrawer.NextLineIcon();
    }

    private void Initiallize()
    {
        mainTextDrawer.Initiallize();

        //�e�L�X�g�t�@�C���̓ǂݍ��݁B_sentences�Ɋi�[
        if (textAsset == null)
        {
            Debug.LogError("�e�L�X�g�t�@�C����������܂���ł���");
            return;
        }
        StringReader reader = new(textAsset.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (line == "") continue;
            _sentences.Add(line);
        }

        unitTime = 0f;

        //�e�L�X�g��\��
        string[] words = SplitText();
        mainTextDrawer.SplitMainText(words);
        if (!(nameTextDrawer == null))
        {
            nameTextDrawer.SplitNameText(words);
        }
        mainTextDrawer.DisplayTextRuby();
    }

    private void TimeKeeper()
    {
        if (unitTime >= intervalTime)
        {
            unitTime -= intervalTime * mainTextDrawer.GetDelayTime();
            mainTextDrawer.Typewriter();
        }
    }

    private string GetCurrentSentence()
    {
        //���݂̍s���擾
        if (lineNumber >= _sentences.Count)
        {
            return null;
        }
        return _sentences[lineNumber];
    }

    private void ChangeText()
    {
        if (_inputSetting.GetDecideKeyUp() || _inputSetting.GetCancelKeyUp())
        {
            DisplayText();
        }
        string[] words = SplitText();
        mainTextDrawer.SplitMainText(words);
        if (!(nameTextDrawer == null))
        {
            nameTextDrawer.SplitNameText(words);
        }
        mainTextDrawer.DisplayTextRuby();
    }

    private void DisplayText()
    {
        if (mainTextDrawer.AllowChangeLine() && unitTime > -0.45f)
        {
            if (0 <= lineNumber && lineNumber <= _sentences.Count - 1)
            {
                //���̍s�ֈړ����A�\�����镶���������Z�b�g
                if (_inputSetting.GetDecideKeyUp() && !(lineNumber >= _sentences.Count - 1))
                {
                    ChangeLine(1);
                    Debug.Log("NextLine");
                }
                else if (_inputSetting.GetCancelKeyUp() && !((lineNumber <= 0)))
                {
                    ChangeLine(-1);
                    Debug.Log("BackLine");
                }
            }
            else
            {
                Debug.Log("SceneEnded");
            }

        }
        else if (unitTime > -0.45f)
        {
            mainTextDrawer.SkipTypewriter();
        }
        else
        {
            //�G���[�΍�B�s�v���͂���B
            unitTime = 0.2f;
        }
        if (unitTime > -0.55f)//�A�ő΍�i�����X�N���[�����j
            unitTime -= 0.35f;
    }

    private void ChangeLine(int increase)
    {
        unitTime = intervalTime;
        lineNumber += increase;
        mainTextDrawer.InitializeLine();
    }

    private string[] SplitText()
    {
        string text = GetCurrentSentence();
        string[] words = text.Split(':');
        //���O������ꍇ�A���O��\���B���O���Ȃ��ꍇ�A���O�\�����\���ɂ���B���O�́u���O:���́v�Ƃ����`���ŋL�q����B
        return words;
    }
}