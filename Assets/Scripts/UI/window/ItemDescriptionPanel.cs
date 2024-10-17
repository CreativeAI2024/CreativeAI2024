using TMPro;
using UnityEngine;

public class ItemDescriptionPanel : MonoBehaviour
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private TextMeshProUGUI textComponent;
    public void SetText(Item item)
    {
        textComponent.text = item.DescriptionText;
    }
}
