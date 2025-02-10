using System.Collections.Generic;
using UnityEngine;

// シーンを切り替えてもキャラチップの表示状態を保存するためにDontDestroySingletonにしている
// 各シーンのCharatipDisplayは、Start()で前回のキャラチップの状態を取得する。
public class CharatipDisplayManager : DontDestroySingleton<CharatipDisplayManager>
{
    // どのシーンからでも、各シーンのCharatipDisplayを文字列で指定して表示状態を変更できるようにするためのDict
    private Dictionary<string, CharatipDisplay> charatipDisplayDict = new();
    // 各CharatipDisplayがStart()で呼び出す。
    public void RegisterCharatipDisplay(CharatipDisplay charatipDisplay)
    {
        if (!charatipDisplayDict.ContainsKey(charatipDisplay.name))
        {
            charatipDisplayDict.Add(charatipDisplay.name, charatipDisplay);
        }
    }
    public bool GetIsVisible(string charatipName)
    {
        if (charatipDisplayDict.ContainsKey(charatipName))
        {
            return charatipDisplayDict[charatipName].gameObject.activeSelf;
        }
        else
        {
            DebugLogger.Log($"このキャラチップ「{charatipName}」はまだManagerに登録されていません。");
            return false;
        }
    }
    public void SetIsVisible(string charatipName, bool isVisible)
    {
        if (!charatipDisplayDict.ContainsKey(charatipName))
        {
            DebugLogger.Log($"失敗: 一度も訪れていないシーンのキャラチップ「{charatipName}」を変更しようとしました。");
        }
        charatipDisplayDict[charatipName].gameObject.SetActive(isVisible);
    }
}