using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionBranch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private GameObject questionBranch;

    public void HideQuestionBranch()
    {
        questionBranch.SetActive(false);
    }

    public void DisplayQuestionBranchText(string str)
    {
        questionBranch.SetActive(true);  //選択肢の表示
        textMeshPro.text = str;
    }
}
