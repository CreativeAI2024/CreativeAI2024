using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWindowCursor : MonoBehaviour
{
    [SerializeField] private Sprite cursorSprite;  //カーソル画像
    [SerializeField] private Sprite questionBranchSprite;  //非選択時の画像
    private InputSetting _inputSetting;
    private int cursorPlace;
    private Image[] questionBranchImage;

    private void Start()
    {
        _inputSetting = InputSetting.Load();
        cursorPlace = 0;
    }
    
    public void SetQuestionBranchImage(Image[] questionBranchImages)
    {
        questionBranchImage = questionBranchImages;
    }

    public void CursorMove(int max)
    {
        if (cursorPlace >= 0 && cursorPlace < max)
        {
            if (_inputSetting.GetBackKeyUp() || _inputSetting.GetRightKeyUp())  //一直線に並べることを想定
            {
                cursorPlace = Mathf.Min(cursorPlace + 1, max - 1);
            }
            else if (_inputSetting.GetForwardKeyUp() || _inputSetting.GetLeftKeyUp())
            {
                cursorPlace = Mathf.Max(cursorPlace - 1, 0);
            }
        }
        for (int i = 0; i < max; ++i)
        {
            questionBranchImage[i].sprite = questionBranchSprite;
        }
        questionBranchImage[cursorPlace].sprite = cursorSprite;
    }

    public int GetCursorPlace()
    {
        return cursorPlace;
    }
}
