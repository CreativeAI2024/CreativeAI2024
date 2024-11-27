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
        cursorPlace = 0;
        if (!PlayerPrefs.GetString("SceneName", "null").Equals("null"))
        {
            cursorMax = 2;
            TitleCursorMove(1);
        }
        else
        {
            cursorMax = 1;
            questionBranches[1].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (_inputSetting.GetBackKeyUp())
        {
            TitleCursorMove(1);
        }
        else if (_inputSetting.GetForwardKeyUp())
        {
            TitleCursorMove(-1);
        }
        if (_inputSetting.GetDecideInputUp())
        {
            switch (cursorPlace)
            {
                case 0:
                    FlagManager.Instance.DeleteFlagFile();
                    PlayerPrefs.DeleteAll();  //セーブデータ初期化
                    SceneManager.LoadScene("mirror_room");
                    break;
                case 1:
                    SceneManager.LoadScene(PlayerPrefs.GetString("SceneName"));
                    break;
            }
        }
    }

    public void TitleCursorMove(int increase)
    {
        cursorPlace = Mathf.Clamp(cursorPlace + increase, 0, cursorMax - 1);
        cursor.CursorMove(rectTransforms[cursorPlace].position);
    }
}
