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
    
    void OnEnable()
    {
        if (itemName.text!=null && item!=null)
        {
            SetButtonText();
        }        
    }
    public void Initialize(Item item, GameObject menuUI, ItemImageWindow itemImageWindow, DescriptionWindow descriptionWindow)
    {
        this.item = item;
        SetButtonText();
        this.itemImageWindow = itemImageWindow;
        this.descriptionWindow = descriptionWindow;
        this.menuUI = menuUI;
    }
    private void SetButtonText()
    {
        itemName.text = item.ItemName + (item.Count>2 ? " × "+item.Count : "");
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
            ConversationTextManager.Instance.Initialize(item.ContentTextFilePath);
        }
    }
    
    public void OnCancelKeyDown()
    {
        menuUI.SetActive(false);
    }
}
