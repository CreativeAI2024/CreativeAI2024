using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetVarCombineMaterial : MonoBehaviour
{
  private InputSetting _inputSetting;
  private ItemInventory itemInventory;
  private CombineRecipeDatabase combineRecipeDatabase;
  private GameObject uiManager;
  private BaseItem thisItem;
  private GameObject confirmYesButton;
  private CSetCombine cSetCombine;
  void Awake()
  {
    itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
    combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
    thisItem = itemInventory.GetItem(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);//押したボタンのテキストからアイテムを取得 かなた質問：ボタンオブジェクトにアイテムボタンを保管するスクリプト作った方がいい？
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
    if (itemInventory.IsContains(combineRecipeDatabase.GetPairItem(thisItem))) //ペアアイテムを持ってるか。引数：渡されたthisItem→recipeから取得
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
        if (itemInventory.IsContains(combineRecipeDatabase.GetPairItem(thisItem))) //ペアアイテムを持ってるか。引数：渡されたthisItem→recipeから取得
        {
          cSetCombine.SetCombineItem(confirmYesButton, thisItem);
        }
      }
    }
  }
}