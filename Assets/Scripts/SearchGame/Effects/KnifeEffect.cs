using System.Collections.Generic;
using UnityEngine;

public class KnifeEffect : MonoBehaviour, IEffectable
{
    [SerializeField] ItemInventory itemInventory;
    [SerializeField] Item item;
    [SerializeField] ItemDatabase itemDatabase;
    public void PlayEffect()
    {
        itemInventory.Add(item);
        List<string> texts = new List<string>();
        texts.Add($"{item.ItemName}を手に入れた<br>{item.DescriptionText}");
        if (itemInventory.IsContains(itemDatabase.GetItem("歴史書")) || itemInventory.IsContains(itemDatabase.GetItem("研究日誌")))
        {
            texts.Add("もう何も見当たらない。");
        }
        ConversationTextManager.Instance.InitializeFromStrings(texts);
        FlagManager.Instance.AddFlag("Knife");
        gameObject.SetActive(false);
    }
}
