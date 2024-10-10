using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetVariablesCombineMaterial : SetVariables
{
  private CombineRecipeDatabase combineRecipeDatabase;
  private GameObject confirmYesButton;
  private CSetCombine cSetCombine;
  new void Start()
  {
    base.Start();
    combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
    cSetCombine = new CSetCombine();
    confirmYesButton = uiManager.GetComponent<GameObjectHolder>().ConfirmYesButton;
    Debug.Log("thisItem: " + thisItem);
    Debug.Log("confirmYesButton: " + confirmYesButton);
    if (itemInventory.IsContains(combineRecipeDatabase.GetPairItem(thisItem))) //ペアアイテムを持ってるか。引数：渡されたthisItem→recipeから取得
    {
      cSetCombine.SetOpenWindowEnabled(gameObject, true);
    }
    else
    {
      cSetCombine.SetOpenWindowEnabled(gameObject, false);
    }
  }

  void OnEnable()
  {
    if (isOnEnableFirstRun)
    {
      isOnEnableFirstRun = false;
    }
    else
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