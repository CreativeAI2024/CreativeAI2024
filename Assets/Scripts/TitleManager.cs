using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private QuestionBranch[] questionBranches;
    [SerializeField] private TextWindowCursor cursor;
    private RectTransform[] rectTransforms;
    private int cursorPlace;
    private InputSetting _inputSetting;
    private int cursorMax = 2;
    // Start is called before the first frame update
    void Start()
    {
        _inputSetting = InputSetting.Load();
        cursorPlace = 0;
        rectTransforms = new RectTransform[questionBranches.Length];
        for (int i = 0; i < questionBranches.Length; i++)
        {
            rectTransforms[i] = questionBranches[i].GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
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
    }

    public void TitleCursorMove(int increase)
    {
        cursorPlace = Mathf.Clamp(cursorPlace + increase, 0, cursorMax - 1);
        cursor.CursorMove(rectTransforms[cursorPlace].position);
    }
}
