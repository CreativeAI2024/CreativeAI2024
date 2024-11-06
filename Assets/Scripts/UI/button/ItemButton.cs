using TMPro;
using UnityEngine;
//TODO: 会話ウィンドウ呼び出し機能作る
//TODO: 初回時にdescriptionとimageの読み込みできてないバグ修正
//TODO: アイテム個数表示機能作成

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
        itemName.text = item.ItemName;
        this.item = item;
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
            // ConversationTextManager.Instantiate.Initialize();
        }
    }
    
    public void OnCancelKeyDown()
    {
        menuUI.SetActive(false);
    }
}
