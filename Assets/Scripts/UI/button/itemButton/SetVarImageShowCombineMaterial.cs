using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetVarImageShowCombineMaterial : MonoBehaviour
{
  private InputSetting _inputSetting;
  private ItemList itemList;
  private CombineRecipeDatabase combineRecipeDatabase;
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
    combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
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
    if (itemList.Search(combineRecipeDatabase.GetPairItem(thisItem.ItemName).ItemName) == true)
    {
      cSetCombine.SetOpenWindowEnabled(gameObject, true);
    }
    else
    {
      cSetCombine.SetOpenWindowEnabled(gameObject, false);
    }
  }
  void Update()
  {
    if (_inputSetting.GetDecideKeyDown())
    {
      if (EventSystem.current.currentSelectedGameObject == gameObject)
      {
        cSetImageShow.SetImage(itemImage);
        if (itemList.Search(combineRecipeDatabase.GetPairItem(thisItem.ItemName).ItemName) == true)
        {
          cSetImageShow.SetNextWindow(confirmWindow);
          cSetCombine.SetCombineItemName(confirmYesButton, thisItem.ItemName);
        }
        else
        {
          cSetImageShow.SetNextWindow(transform.parent.parent.gameObject);
        }
      }
    }
  }
}