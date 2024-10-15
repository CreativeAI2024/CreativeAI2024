using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombineButton : ItemActionButton
{
    private CombineRecipeDatabase combineRecipeDatabase;
    private CombineItems combineItems;
    private Selectable selectable;
    private TextMeshProUGUI buttonText;
    void Start()
    {
        BaseStart();
        combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
        combineItems = gameObjectHolder.ConfirmYesButton.GetComponent<CombineItems>();
        selectable = transform.GetComponent<Selectable>();
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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
            }
        }
    }

    private void Ready()
    {
        if (HasPairItem(thisItem))
        {
            SetEnabled(true);
        }
        else
        {
            SetEnabled(false);
        }
    }

    private void SetEnabled(bool isEnabled)
    {
        selectable.enabled = isEnabled;
        buttonText.color = isEnabled ? Color.white : Color.grey;
    }


    private bool HasPairItem(Item item) => itemInventory.IsContains(combineRecipeDatabase.GetPairIngredient(item));
}