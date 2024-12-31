using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionBranch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private GameObject questionBranch;

    public void SetVisibleQuestionBranch(bool isVisible)
    {
        questionBranch.SetActive(isVisible);  //選択肢の表示
    }

    public void QuestionBranchText(string str)
    {
        textMeshPro.text = str;
    }
}
