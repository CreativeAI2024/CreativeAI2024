using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetVarCombineMaterial : MonoBehaviour
{
  private InputSetting _inputSetting;
  private ItemList itemList;
  private CombineRecipeList combineRecipeList;
  private GameObject uiManager;
  private BaseItem thisItem;
  private GameObject confirmYesButton;
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
    confirmYesButton = uiManager.GetComponent<GameObjectHolder>().ConfirmYesButton;
  }

  void OnEnable()
  {
    if (itemList.Search(combineRecipeList.GetPairItem(thisItem.ItemName).ItemName) == true)
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
        if (itemList.Search(combineRecipeList.GetPairItem(thisItem.ItemName).ItemName) == true)
        {
          cSetCombine.SetCombineItemName(confirmYesButton, thisItem.ItemName);
        }
      }
    }
  }
}