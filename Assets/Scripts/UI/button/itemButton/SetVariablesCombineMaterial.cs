using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetVariablesCombineMaterial : SetVariables
{
  private CombineRecipeDatabase combineRecipeDatabase;
  private GameObject confirmYesButton;
  private CSetCombine cSetCombine;
      //TODO: SetVariablesImageShowCombineMaterialいらない。２つアタッチすればいい。
    //TODO: 合成ボタン配置
    //TODO: 合成ボタンのcurrentWindow配置
    //TODO: 合成ボタンのnextWindow配置
    //TODO: アイテムを確認ウィンドウに渡す

  new void Start()
  {
    GameObjectHolder gameObjectHolder = GameObject.FindWithTag("UIManager").GetComponent<GameObjectHolder>();
    base.Start();
    Transform actionWindowButtons = transform.GetChild(1).GetChild(0);
    GameObject imageShowButton = Instantiate(gameObjectHolder.ImageShowButtonPrefab, actionWindowButtons);
    OpenWindow openWindow = imageShowButton.GetComponent<OpenWindow>();
    openWindow.currentWindow = gameObjectHolder.ItemWindow;
    openWindow.nextWindow = gameObjectHolder.ConfirmWindow;
    combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
    confirmYesButton = gameObjectHolder.ConfirmYesButton;
    cSetCombine = new CSetCombine();
    Ready();
  }

  void OnEnable()
  {
    if (isOnEnableFirstRun)
    {
      isOnEnableFirstRun = false;
    }
    else
    {
      Ready();
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
  private void Ready()
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