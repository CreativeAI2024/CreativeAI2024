using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConversationTextManager : MonoBehaviour
{
    [SerializeField] private MainTextDrawer mainTextDrawer;
    [SerializeField] private NameTextDrawer nameTextDrawer;
    [SerializeField] private TextAsset textAsset;

    [SerializeField] private float intervalTime;
    private float unitTime;
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
        unitTime += Time.deltaTime;
        TimeKeeper();

        // z�L�[�������ꂽ�Ƃ��A���̍s�ֈړ�
        ChangeLine();

        //���̍s�֐i�ރA�C�R���̕\����\��
        mainTextDrawer.GoToTheNextLineIcon();
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
            mainTextDrawer._sentences.Add(line);
        }

        unitTime = 0f;

        //�ꕶ�����\�����邽�߁A�ŏ���0�����ɐݒ�
        mainTextDrawer.SetMaxVisibleCharacters(0);
        //�e�L�X�g��\��
        SplitText();
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
        if (mainTextDrawer.GetLineNumber() >= mainTextDrawer._sentences.Count)
        {
            return null;
        }
        return mainTextDrawer._sentences[mainTextDrawer.GetLineNumber()];
    }

    private void ChangeLine()
    {
        if (_inputSetting.GetDecideKeyUp() || _inputSetting.GetCancelKeyUp())
        {
            DisplayText();
        }
        SplitText();
        mainTextDrawer.DisplayTextRuby();
    }

    private void DisplayText()
    {
        if (mainTextDrawer.CanGoToTheNextLine() && unitTime > -0.45f)
        {
            if (_inputSetting.GetDecideKeyUp())
            {
                unitTime = intervalTime;
                mainTextDrawer.GoToTheNextLine();
            }
            else if (_inputSetting.GetCancelKeyUp())
            {
                unitTime = intervalTime;
                mainTextDrawer.GoToTheFormerLine();
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

    private void SplitText()
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
        mainTextDrawer.SetMainText(lineText);
    }
}
