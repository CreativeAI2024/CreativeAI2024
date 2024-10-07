using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetVarImageShowCombineMaterial : MonoBehaviour
{
  private InputSetting _inputSetting;
  private ItemList itemList;
  private CombineRecipeList combineRecipeList;
  private GameObject uiManager;
  private BaseItem thisItem;
  private Sprite itemImage;
  private GameObject confirmWindow;
  private GameObject confirmYesButton;
  private GameObject itemImageScreen;
  private CSetImageShow cSetImageShow;
  private CSetCombine cSetCombine;

  void Awake()
  {
    itemList = Resources.Load<ItemList>("Items/ItemList");
    combineRecipeList = Resources.Load<CombineRecipeList>("Items/CombineRecipes/CombineRecipeList");
    thisItem = itemList.Search(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
    cSetCombine = new CSetCombine();
}
  void Start()
  {
    _inputSetting = InputSetting.Load();
    uiManager = GameObject.FindWithTag("UIManager");
    string itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    itemImage = ((ImageShowItem)itemList.Search(itemName)).Image;
    confirmWindow = uiManager.GetComponent<GameObjectHolder>().ConfirmWindow;
    confirmYesButton = uiManager.GetComponent<GameObjectHolder>().ConfirmYesButton;
    itemImageScreen = uiManager.GetComponent<GameObjectHolder>().ItemImageScreen;
    cSetImageShow = new CSetImageShow(itemImageScreen);
  }

  void OnEnable()
  {
    if (itemList.Search(combineRecipeList.GetPairItem(thisItem.ItemName).ItemName) == true)
    {
      cSetCombine.SetEnabled(gameObject, true);
    }
    else
    {
      cSetCombine.SetEnabled(gameObject, false);
    }
  }
  void Update()
  {
    if (_inputSetting.GetDecideKeyDown())
    {
      if (EventSystem.current.currentSelectedGameObject == gameObject)
      {
        itemImageScreen.GetComponent<Image>().sprite = itemImage;
        if (itemList.Search(combineRecipeList.GetPairItem(thisItem.ItemName).ItemName) == true)
        {
          cSetImageShow.SetNextWindow(confirmWindow);
          cSetCombine.SetItemName(confirmYesButton, thisItem.ItemName);
        }
        else
        {
          cSetImageShow.SetNextWindow(transform.parent.parent.gameObject);
        }
      }
    }
  }
}