using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionBranch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private GameObject questionBranch;

    public void DisableQuestionBranch()
    {
        questionBranch.SetActive(false);
    }

    public void EnableQuestionBranch()
    {
        questionBranch.SetActive(true);  //選択肢の表示
    }

    public void QuestionBranchText(string str)
    {
        textMeshPro.text = str;
    }
}
