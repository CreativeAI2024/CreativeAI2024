using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private TextMeshProUGUI textComponent;
    public void SetText(Item item)
    {
        textComponent.text = item.DescriptionText;
    }
}
