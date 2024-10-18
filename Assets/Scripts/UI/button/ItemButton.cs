using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IDecideCancelObject
{
    private Item item;
    private ItemActionWindow itemActionWindow;
    private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI button;
    public void Initialize(Item item, ItemActionWindow itemActionWindow, TextMeshProUGUI itemDescription)
    {
        this.item = item;
        this.itemActionWindow = itemActionWindow;
        this.itemDescription = itemDescription;
        button.text = item.ItemName;
    }
    public void OnDecideKeyDown()
    {
    }
    public void OnCancelKeyDown()
    {
    }
}
