using UnityEngine;

public class ItemBookEffect : MonoBehaviour, IEffectable
{
    [SerializeField] ItemInventory itemInventory;
    [SerializeField] Item item;
    [SerializeField] string jsonFile;
    public void PlayEffect()
    {
        itemInventory.Add(item);
        ConversationTextManager.Instance.InitializeFromJson(jsonFile);
    }
}
