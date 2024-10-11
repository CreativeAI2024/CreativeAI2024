using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombineButton : ItemButton
{
    private CombineRecipeDatabase combineRecipeDatabase;
    private CombineItems combineItems;
    new void Start()
    {
        base.Start();
        combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");


        combineItems = gameObjectHolder.ConfirmYesButton.GetComponent<CombineItems>();
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
                if (HasPairItem(thisItem)) //ペアアイテムを持ってるか。引数：渡されたthisItem→recipeから取得
                {
                    combineItems.MaterialItem = thisItem;
                }
            }
        }
    }

    private void Ready()
    {
        if (HasPairItem(thisItem))
        {
            SetOpenWindowEnabled(true);
        }
        else
        {
            SetOpenWindowEnabled(false);
        }
    }

    private void SetOpenWindowEnabled(bool isEnabled)
    {
        openWindow.enabled = isEnabled;
    }


    private bool HasPairItem(Item item) => itemInventory.IsContains(combineRecipeDatabase.GetPairItem(item));
}