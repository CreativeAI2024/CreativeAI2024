using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConversationTextManager : MonoBehaviour
{
    public static ConversationTextManager Instance { get; private set; }

    public MainTextDrawer  mainTextDrawer;
    public NameTextDrawer  nameTextDrawer;
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
        ChangeLine(KeyCode.Z, KeyCode.X);

        //���̍s�֐i�ރA�C�R���̕\����\��
        mainTextDrawer.GoToTheNextLineIcon();
    }

    public void Initiallize()
    {
        Instance = this;
        //���L�O�s�͂��܂�Ӗ����Ȃ��B
        /*_mainTextObject = _mainTextObject.GetComponent<TextMeshProUGUI>();
        _nameTextObject = _nameTextObject.GetComponent<TextMeshProUGUI>();
        rb = _mainTextObject.GetComponent<TextMeshProRuby>();*/

        mainTextDrawer = mainTextDrawer.GetComponent<MainTextDrawer>();
        nameTextDrawer = nameTextDrawer.GetComponent<NameTextDrawer>();

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


    public void ChangeLine(KeyCode keyNext, KeyCode keyFormer)
    {

        if (Input.GetKeyUp(keyNext))
        {
            //�S�����\������Ă���ꍇ�A���̍s�ֈړ�
            if (mainTextDrawer.CanGoToTheNextLine() && _time > -0.45f)
            {
                mainTextDrawer.GoToTheNextLine();
                MainText();
                mainTextDrawer.DisplayText();

            }
            else if (_time > -0.45f)
            {
                //�S�����\������Ă��Ȃ��ꍇ�ɃL�[���������Ƃ��A�S����\��
                mainTextDrawer._mainTextObject.maxVisibleCharacters = mainTextDrawer._displayedSentenceLength = mainTextDrawer._sentenceLength;
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
        else if (Input.GetKeyUp(keyFormer))
        {
            if (mainTextDrawer.CanGoToTheNextLine() && _time > -0.45f)
            {
                //GameManager.Instance.audioManager.PageSE();
                mainTextDrawer.GoToTheFormerLine();
                MainText();
                mainTextDrawer.DisplayText();

            }
            else if (_time > -0.45f)
            {
                //�S�����\������Ă��Ȃ��ꍇ�ɃL�[���������Ƃ��A�S����\��
                mainTextDrawer._mainTextObject.maxVisibleCharacters = mainTextDrawer._displayedSentenceLength = mainTextDrawer._sentenceLength;
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
        string[] words = text.Split(":");
        //���O������ꍇ�A���O��\���B���O���Ȃ��ꍇ�A���O�\�����\���ɂ���B���O�́u���O:���́v�Ƃ����`���ŋL�q����B
        if (words.Length > 1)
        {
            nameTextDrawer.NameText(words[0]);
            mainTextDrawer._mainTextObject.text = words[1];
        }
        else
        {
            nameTextDrawer._nameTextPrefab.SetActive(false);
            mainTextDrawer._mainTextObject.text = text;
        }
    }
}
