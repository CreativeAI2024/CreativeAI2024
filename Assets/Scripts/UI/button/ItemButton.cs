using TMPro;
using UnityEngine;

public class ItemButton : MonoBehaviour, IPushedObject, IFocusedObject
{
    [SerializeField] private TextMeshProUGUI itemName;
    private ItemImageWindow itemImageWindow;
    private DescriptionWindow descriptionWindow;
    private Item item;
    private GameObject menuUI;
    public int Index => transform.GetSiblingIndex();

    public void Initialize(Item item, GameObject menuUI, ItemImageWindow itemImageWindow, DescriptionWindow descriptionWindow)
    {
        this.item = item;
        itemName.text = item.ItemName;
        this.itemImageWindow = itemImageWindow;
        this.descriptionWindow = descriptionWindow;
        this.menuUI = menuUI;
    }
    public void OnFocused()
    {
        itemImageWindow.SetImage(item.Sprite);
        descriptionWindow.SetText(item.DescriptionText);
    }
    public void OnDecideKeyDown()
    {
        if (item.HasContentText())
        {
            menuUI.SetActive(false);
            ConversationTextManager.Instance.InitializeFromJson(item.ContentTextFilePath);
        }
    }
    
    public void OnCancelKeyDown()
    {
        menuUI.SetActive(false);
    }
}
