using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetVarImageShowCombineMaterial : MonoBehaviour
{
  private InputSetting _inputSetting;
  private ItemInventory itemInventory;
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
    itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
    combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
    thisItem = itemInventory.GetItem(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
    cSetCombine = new CSetCombine();
}
  void Start()
  {
    _inputSetting = InputSetting.Load();
    uiManager = GameObject.FindWithTag("UIManager");
    string itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    itemImage = ((ImageShowItem)itemInventory.GetItem(itemName)).Image;
    confirmWindow = uiManager.GetComponent<GameObjectHolder>().ConfirmWindow;
    confirmYesButton = uiManager.GetComponent<GameObjectHolder>().ConfirmYesButton;
    itemImageScreen = uiManager.GetComponent<GameObjectHolder>().ItemImageScreen;
    cSetImageShow = new CSetImageShow(itemImageScreen);
  }

  void OnEnable()
  {
    if (itemInventory.IsContains(combineRecipeDatabase.GetPairItem(thisItem)))
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
        if (itemInventory.IsContains(combineRecipeDatabase.GetPairItem(thisItem)))
        {
          cSetImageShow.SetNextWindow(confirmWindow);
          cSetCombine.SetCombineItem(confirmYesButton, thisItem);
        }
        else
        {
          cSetImageShow.SetNextWindow(transform.parent.parent.gameObject);
        }
      }
    }
  }
}