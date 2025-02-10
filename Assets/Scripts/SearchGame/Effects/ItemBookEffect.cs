using UnityEngine;

public class ItemBookEffect : MonoBehaviour, IEffectable
{
    [SerializeField] ItemInventory itemInventory;
    [SerializeField] Item item;
    [SerializeField] ItemDatabase itemDatabase;
    public void PlayEffect()
    {
        itemInventory.Add(item);
        ConversationTextManager.Instance.InitializeFromString($"{item.ItemName}を手に入れた。");
        ConversationTextManager.Instance.InitializeFromJson(item.ContentTextFilePath);
        if (itemInventory.IsContains(itemDatabase.GetItem("Knife")))
        {
            ConversationTextManager.Instance.InitializeFromString($"もう何も見つからない。");
        }
        gameObject.SetActive(false);
    }
}
