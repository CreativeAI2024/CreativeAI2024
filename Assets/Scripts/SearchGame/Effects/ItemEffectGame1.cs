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
        texts.Add($"{item.ItemName}を手に入れた<br>{item.DescriptionText}");
        if (itemInventory.IsContains(itemDatabase.GetItem("虫の死骸")) && itemInventory.IsContains(itemDatabase.GetItem("空の瓶")))
        {
            itemInventory.TryCombine(itemDatabase.GetItem("虫の死骸"));
            texts.Add($"虫の死骸を空の瓶へ入れ、{itemInventory.GetItem("虫入り瓶").ItemName}を作成した。<br>{itemInventory.GetItem("虫入り瓶").DescriptionText}");
            texts.Add("もう何も見当たらない。");
            FlagManager.Instance.AddFlag("Worm");
        }
        ConversationTextManager.Instance.InitializeFromStrings(texts);
        gameObject.SetActive(false);
    }
}
