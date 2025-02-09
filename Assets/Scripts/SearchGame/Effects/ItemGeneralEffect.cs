using UnityEngine;

public class ItemGeneralEffect : MonoBehaviour, IEffectable
{
    [SerializeField] ItemInventory itemInventory;
    [SerializeField] Item item;
    public void PlayEffect()
    {
        itemInventory.Add(item);
        ConversationTextManager.Instance.InitializeFromString($"{item.ItemName}を手に入れた。");
    }
}
