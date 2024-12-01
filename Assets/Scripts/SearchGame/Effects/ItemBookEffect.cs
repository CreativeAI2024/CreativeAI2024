using UnityEngine;

public class ItemBookEffect : MonoBehaviour, IEffectable
{
    [SerializeField] ItemInventory itemInventory;
    [SerializeField] Item item;
    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] string jsonFile;
    public void PlayEffect()
    {
        itemInventory.Add(item);
        if (itemInventory.IsContains(itemDatabase.GetItem("ナイフ")))
        {
            jsonFile = $"{jsonFile}_any";
        }
        ConversationTextManager.Instance.InitializeFromJson(jsonFile);
        gameObject.SetActive(false);
    }
}
