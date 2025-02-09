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
        ConversationTextManager.Instance.InitializeFromString($"{item.ItemName}を手に入れた。");
        if (itemInventory.IsContains(itemDatabase.GetItem("OtherWorldBook")) || itemInventory.IsContains(itemDatabase.GetItem("OtherWorldNote")))
        {
            ConversationTextManager.Instance.InitializeFromString($"もう何も見つからない。");
        }
        FlagManager.Instance.AddFlag("Knife");
        gameObject.SetActive(false);
    }
}
