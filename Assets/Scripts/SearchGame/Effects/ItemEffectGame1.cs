using System.Collections.Generic;
using UnityEngine;

public class ItemEffectGame1 : MonoBehaviour, IEffectable
{
    [SerializeField] ItemInventory itemInventory;
    [SerializeField] Item item;
    [SerializeField] ItemDatabase itemDatabase;
    public void PlayEffect()
    {
        itemInventory.Add(item);
        List<string> texts = new List<string>();
        texts.Add($"{item.ItemName}を手に入れた");
        if (itemInventory.IsContains(itemDatabase.GetItem("BugsCorpse")) && itemInventory.IsContains(itemDatabase.GetItem("EmptyJar")))
        {
            itemInventory.TryCombine(itemDatabase.GetItem("BugsCorpse"));
            foreach (var v in itemInventory.GetItems()) DebugLogger.Log(v, DebugLogger.Colors.Red);
            texts.Add($"虫の死骸を空の瓶へ入れ、{itemInventory.GetItem("BugsInJar").ItemName}を作成した。");
            texts.Add("もう何も見当たらない。");
            FlagManager.Instance.AddFlag("Worm");
        }
        ConversationTextManager.Instance.InitializeFromStrings(texts);
        gameObject.SetActive(false);
    }
}
