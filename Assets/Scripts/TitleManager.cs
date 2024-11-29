using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private QuestionBranch[] questionBranches;
    [SerializeField] private TextWindowCursor cursor;
    private RectTransform[] rectTransforms;
    private int cursorPlace;
    private InputSetting _inputSetting;
    private int cursorMax;

    private const int NewGame = 0;
    private const int LoadGame = 1;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetString("SceneName", "reference_room");
        _inputSetting = InputSetting.Load();
        rectTransforms = new RectTransform[questionBranches.Length];
        for (int i = 0; i < questionBranches.Length; i++)
        {
            rectTransforms[i] = questionBranches[i].GetComponent<RectTransform>();
        }
        DebugLogger.Log(PlayerPrefs.GetString("SceneName", "null"));
        cursorPlace = NewGame;
        if (!PlayerPrefs.GetString("SceneName", "null").Equals("null"))
        {
            cursorMax = LoadGame + 1;
            TitleCursorMove(LoadGame);
        }
        else
        {
            cursorMax = NewGame +1 ;
            questionBranches[LoadGame].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (_inputSetting.GetBackKeyDown())
        {
            TitleCursorMove(LoadGame);
        }
        else if (_inputSetting.GetForwardKeyDown())
        {
            TitleCursorMove(NewGame);
        }
        if (_inputSetting.GetDecideInputDown())
        {
            switch (cursorPlace)
            {
                case NewGame:
                    GoNewGame();
                    break;
                case LoadGame:
                    GoLoadGame();
                    break;
            }
        }
    }

    public void TitleCursorMove(int selectCursorPlace)
    {
        cursorPlace = Mathf.Clamp(selectCursorPlace, 0, cursorMax - 1);
        cursor.CursorMove(rectTransforms[cursorPlace].position);
    }

    private void GoNewGame()
    {
        FlagManager.Instance.DeleteFlagFile();
        PlayerPrefs.DeleteAll();  //セーブデータ初期化
        SceneManager.LoadScene("mirror_room");
    }

    private void GoLoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("SceneName"));
    }
}
