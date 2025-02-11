using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingGameJudge : MonoBehaviour
{
    [SerializeField, HeaderAttribute("単位:ms")] private float judgeGreat;  //判定その１
    [SerializeField] private float judgeGood;   //判定その２
    
    public void JudgeScore(float score)
    {
        if (score <= judgeGood)
        {
            if (score <= judgeGreat)
            {
                ConversationTextManager.Instance.InitializeFromString("とてもうまくできた。");
            }
            else
            {
                ConversationTextManager.Instance.InitializeFromString("少しミスしたが、問題ない。");
            }
            ChangeSuccessFlag("Progress9");
        }
        else
        {
            ConversationTextManager.Instance.InitializeFromString("…………………………………………");
            ChangeSuccessFlag("Progress8");
        }
    }

    public void ChangeSuccessFlag(string flagName)
    {
        if (FlagManager.Instance.HasFlag("StartTimingGame2"))
        {
            FlagManager.Instance.AddFlag(flagName);
        }
        else if (FlagManager.Instance.HasFlag("StartTimingGame1"))
        {
            FlagManager.Instance.AddFlag("ClearMixing1");
        }
    }

}
