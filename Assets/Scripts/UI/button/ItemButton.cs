using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IFocusedObject
{
    private Item item;
    private TextMeshProUGUI itemDescriptionPanel;
    [SerializeField] private TextMeshProUGUI button;
    public void Initialize(Item item, TextMeshProUGUI itemDescriptionPanel)
    {
        this.item = item;
        this.itemDescriptionPanel = itemDescriptionPanel;
        button.text = item.ItemName;
    }
    public void OnDirectionKeyDown()
    {
        itemDescriptionPanel.text = item.DescriptionText;
    }
}
