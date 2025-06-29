using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private QuestionBranch[] questionBranches;
    [SerializeField] private TextWindowCursor cursor;
    [SerializeField] private ItemInitializer itemInitializer;
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
        if (PlayerPrefs.GetString("SceneName", "null").Equals("null") || FlagManager.Instance.HasFlag("Ending"))
        {
            cursorMax = NewGame + 1;
            questionBranches[LoadGame].gameObject.SetActive(false); 
        }
        else
        {
            cursorMax = LoadGame + 1;
            TitleCursorMove(LoadGame);
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
        if (!Input.GetMouseButtonDown(0) && _inputSetting.GetDecideInputDown())
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
        itemInitializer.DeleteFlagFile();
        FlagManager.Instance.DeleteFlagFile();
        PlayerPrefs.DeleteAll();  //セーブデータ初期化
        FlagManager.Instance.SetReiStatus(0);
        SceneManager.LoadScene("mirror_room");
    }

    private void GoLoadGame()
    {
        itemInitializer.ItemInitialize();
        SceneManager.LoadScene(PlayerPrefs.GetString("SceneName"));
    }
}
