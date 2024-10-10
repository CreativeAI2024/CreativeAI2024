using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetVariablesImageShowCombineMaterial : SetVariables
{
  private CombineRecipeDatabase combineRecipeDatabase;
  private Sprite itemImage;
  private GameObject itemImageScreen;
  private GameObject confirmWindow;
  private GameObject confirmYesButton;
  private CSetImageShow cSetImageShow;
  private CSetCombine cSetCombine;

  new void Start()
  {
    base.Start();
    combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
    itemImage = ((ImageShowItem)thisItem).Image;
    itemImageScreen = uiManager.GetComponent<GameObjectHolder>().ItemImageScreen;
    confirmWindow = uiManager.GetComponent<GameObjectHolder>().ConfirmWindow;
    confirmYesButton = uiManager.GetComponent<GameObjectHolder>().ConfirmYesButton;
    cSetImageShow = new CSetImageShow(itemImageScreen);
    cSetCombine = new CSetCombine();
    if (itemInventory.IsContains(combineRecipeDatabase.GetPairItem(thisItem)))
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
      if (itemInventory.IsContains(combineRecipeDatabase.GetPairItem(thisItem)))
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