using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;


namespace NovelGame
{
    public class MainTextController : MonoBehaviour
    {
        public static MainTextController Instance { get; private set; }
        [SerializeField] public TextMeshProUGUI _mainTextObject;
        [SerializeField] public TextMeshProUGUI _nameTextObject;

        [SerializeField] public GameObject _mainTextPrefab;
        [SerializeField] GameObject _nameTextPrefab;

        [SerializeField] public TextMeshProRuby rb;

        [SerializeField] Animator animator;
        [SerializeField] public GameObject _nextPageIcon;
        [SerializeField] Image iconObject;

        private AudioSource audioSource;
        [SerializeField] AudioClip audioClipTyping;
        [SerializeField] AudioClip audioClipNextLine;

        public static int _displayedSentenceLength = -1;
        public int _sentenceLength;
        public static float _time;
        public static float _feedTime = 0.04f;
        public static float _waitTime;
        public int lineNumber = 0;

        [SerializeField] string _nextScene;
        //public string _loadingTextFile;
        public TextAsset _textFile;

        public List<string> _sentences = new List<string>();
        //string _textFilePath ="Sample";

        // Start is called before the first frame update
        void Start()
        {
            Initiallize();
        }

        void Update()
        {

            if (rb == null)
            {
                //エラー対策
                print("rb is null");
                rb = _mainTextObject.GetComponent<TextMeshProRuby>();
            }

            // 単位時間 feedTimeごとに文章を１文字ずつ表示する
            Typewriter();

            // zキーが離されたとき、次の行へ移動
            ChangeLine(KeyCode.Z,KeyCode.X);

            //次の行へ進むアイコンの表示非表示
            GoToTheNextLineIcon();
            

        }

        //start()に置くための初期化
        public void Initiallize()
        {
            Instance = this;
            //下記三行はあまり意味がない。
            _mainTextObject = _mainTextObject.GetComponent<TextMeshProUGUI>();
            _nameTextObject = _nameTextObject.GetComponent<TextMeshProUGUI>();
            rb = _mainTextObject.GetComponent<TextMeshProRuby>();

            //テキストファイルの読み込み。_sentencesに格納
            //var tex = EditorGUILayout.ObjectField("text", null, typeof(TextAsset), true) as TextAsset;
            //var path = AssetDatabase.GetAssetPath(tex);
            //TextAsset _textFile = Resources.Load<TextAsset>(_loadingTextFile);
            //TextAsset _textFile = Resources.Load<TextAsset>("Texts/" + _textFilePath);
            if (_textFile == null)
            {
                Debug.LogError("テキストファイルが見つかりませんでした");
                return;
            }
            StringReader reader = new StringReader(_textFile.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                if (line == "") continue;
                _sentences.Add(line);
            }

            _time = 0f;

            //一文字ずつ表示するため、最初は0文字に設定
            _mainTextObject.maxVisibleCharacters = 0;
            //テキストを表示
            DisplayText();

            //ルビ振りに関する設定
            rb.FixedLineHeight = true;
            rb.AutoMarginTop = true;

            audioSource = GetComponent<AudioSource>();
        }

        // 単位時間 feedTimeごとに文章を１文字ずつ表示する
        public void Typewriter()
        {
            _time += Time.deltaTime;
            if (_time >= _feedTime)
            {
                _time -= _feedTime;

                if (!CanGoToTheNextLine())
                {
                    string sentence = _mainTextObject.GetParsedText();

                    //_displayedSentenceLengthでmaxVisibleCharactersを制御。今回の場合、ここは直接変更にしてもいいかもしれない。
                    _displayedSentenceLength++;

                    //参照漏れの防止
                    if (_displayedSentenceLength > 0 && _mainTextObject.GetParsedText().Length > _displayedSentenceLength - 1)
                    {
                        //前回よりテキストを一文字多く表示する。
                        _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
                        //テキスト音を鳴らす
                        if (_displayedSentenceLength % 3 == 0)
                        {
                            //テキスト音を鳴らすためのコード
                            //GameManager.Instance.audioManager.CharSE();
                            audioSource.clip = audioClipTyping;
                            audioSource.PlayOneShot(audioSource.clip);
                        }
                        if (sentence[_displayedSentenceLength - 1].Equals('。') || sentence[_displayedSentenceLength - 1].Equals('！') || sentence[_displayedSentenceLength - 1].Equals('？'))
                        {
                            //、と。で表示速度を変える。
                            _time -= 0.4f;
                        }
                        else if (sentence[_displayedSentenceLength - 1].Equals('、'))
                        {
                            _time -= 0.2f;
                        }
                    }
                }
            }
        }

        public void ChangeLine(KeyCode keyNext, KeyCode keyFormer)
        {

            if (Input.GetKeyUp(keyNext))
            {
                //全文が表示されている場合、次の行へ移動
                if (CanGoToTheNextLine() && _time > -0.45f)
                {
                    //GameManager.Instance.audioManager.PageSE();
                    GoToTheNextLine();
                    DisplayText();
                    audioSource.clip = audioClipNextLine;
                    audioSource.PlayOneShot(audioSource.clip);
                }
                else if (_time > -0.45f)
                {
                    //全文が表示されていない場合に右クリックしたとき、全文を表示
                    _mainTextObject.maxVisibleCharacters = _displayedSentenceLength = _sentenceLength;
                }
                else
                {
                    //エラー対策。不要説はある。
                    _time = 0.2f;
                }
                if (_time > -0.55f)//連打対策（爆速スクロール等）
                    _time -= 0.35f;

            }
            else if (Input.GetKeyUp(keyFormer))
            {
                //全文が表示されている場合、次の行へ移動
                if (CanGoToTheNextLine() && _time > -0.45f)
                {
                    //GameManager.Instance.audioManager.PageSE();
                    GoToTheFormerLine();
                    DisplayText();
                }
                else if (_time > -0.45f)
                {
                    //全文が表示されていない場合に右クリックしたとき、全文を表示
                    _mainTextObject.maxVisibleCharacters = _displayedSentenceLength = _sentenceLength;
                }
                else
                {
                    //エラー対策。不要説はある。
                    _time = 0.2f;
                }
                if (_time > -0.55f)//連打対策（爆速スクロール等）
                    _time -= 0.35f;

            }
        
        }

        //次の行へ進むアイコンの表示非表示
        public void GoToTheNextLineIcon()
        {
            if (!CanGoToTheNextLine())
            {
                //次の行へ進むことができない場合、次の行へ進むアイコンを非表示にする
                _nextPageIcon.SetActive(false);
                if (animator.enabled == true)
                {
                    animator.enabled = false;
                }
            }
            else if (CanGoToTheNextLine())
            {
                //次の行へ進むことができる場合、次の行へ進むアイコンを表示する
                if (MainTextController._displayedSentenceLength > 0)
                {
                    //アイコンの位置を設定
                    Vector2 textPosition = lastTextPosition();
                    if (textPosition == Vector2.zero) return;
                    textPosition.x += 25f;
                    RectTransform iconTransform = iconObject.GetComponent<RectTransform>();
                    iconTransform.anchoredPosition = textPosition;
                }
                _nextPageIcon.SetActive(true);
                if (animator.enabled == false)
                {
                    animator.enabled = true;
                }
            }
        }

        // その行の、すべての文字が表示されていなければ、まだ次の行へ進むことはできない
        public bool CanGoToTheNextLine()
        {
            string sentence_ = MainText(GetCurrentSentence());
            string sentence = _mainTextObject.GetParsedText();
            _sentenceLength = sentence.Length;
            return (_displayedSentenceLength > sentence.Length - 1);
        }

        // 次の行へ移動
        public void GoToTheNextLine()
        {
            _time = 0.04f;
            if (lineNumber < _sentences.Count - 1)
            {
                //次の行へ移動し、表示する文字数をリセット
                lineNumber++;
                _mainTextObject.maxVisibleCharacters = 0;
                _displayedSentenceLength = 0;
            }else{
                //Debug.Log("STOP!");
                SceneManager.LoadScene(_nextScene);
            }
        }
        // 前の行へ移動
        public void GoToTheFormerLine()
        {
            _time = 0.04f;
            if (lineNumber > 0)
            {
                //前の行へ移動し、表示する文字数をリセット
                lineNumber--;
                _mainTextObject.maxVisibleCharacters = 0;
                _displayedSentenceLength = 0;
            }
            else
            {
                //Debug.Log("STOP!");
                //SceneManager.LoadScene(_nextScene);
            }
        }

        // テキストを表示
        public void DisplayText()
        {
            if (rb == null)
            {
                rb = _mainTextObject.GetComponent<TextMeshProRuby>();
            }
            //テキストを取得し、表示。今回はrb.Textに格納された文字が表示される。
            string text = GetCurrentSentence();
            rb.Text = MainText(text);
        }

        public string MainText(string str)
        {
            string text = GetCurrentSentence();
            string[] words = str.Split(":");
            //名前がある場合、名前を表示。名前がない場合、名前表示を非表示にする。名前は「名前:文章」という形式で記述する。
            if (words.Length > 1)
            {
                NameText(words[0]);
                return words[1];
            }
            else
            {
                _nameTextPrefab.SetActive(false);
                return str;
            }
        }

        public void NameText(string str)
        {
            //名前を表示する
            _nameTextPrefab.SetActive(true);
            _nameTextObject.text = str;
        }

        public string GetCurrentSentence()
        {
            //現在の行を取得
            if (lineNumber >= _sentences.Count)
            {
                return null;
            }
            return _sentences[lineNumber];
        }

        public Vector2 lastTextPosition()
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
}