using UnityEngine;

public class ItemBookEffect : MonoBehaviour, IEffectable
{
    [SerializeField] ItemInventory itemInventory;
    [SerializeField] Item item;
    [SerializeField] ItemDatabase itemDatabase;
    public void PlayEffect()
    {
        itemInventory.Add(item);
        DebugLogger.Log($"fileName: {item.name}_get");
        ConversationTextManager.Instance.InitializeFromJson($"{item.name}_get");
        // ここでUniTaskで止めたら良さそう？
        if (itemInventory.IsContains(itemDatabase.GetItem("Knife")))
        {
            ConversationTextManager.Instance.InitializeFromString($"もう何も見つからない。");
        }
        gameObject.SetActive(false);
    }
}
