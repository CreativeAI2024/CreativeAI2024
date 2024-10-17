using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombineButton : ItemActionButton
{
    private CombineRecipeDatabase combineRecipeDatabase;
    [SerializeField] private CombineItems combineItems;
    private Selectable selectable;
    private TextMeshProUGUI buttonText;
    protected override void OnStart()
    {
        combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
        selectable = transform.GetComponent<Selectable>();
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Ready();
    }

    void OnEnable()
    {
        if (isOnEnableFirstRun)
        {
            isOnEnableFirstRun = false;
            return;
        }
        Ready();
    }
    private void Ready()
    {
        SetEnabled(HasPairItem(thisItem));
    }

    private void SetEnabled(bool isEnabled)
    {
        selectable.enabled = isEnabled;
        buttonText.color = isEnabled ? Color.white : Color.grey;
    }
    private bool HasPairItem(Item item) => itemInventory.IsContains(combineRecipeDatabase.GetPairIngredient(item));

    public override void OnDecideKeyDown()
    {
        combineItems.MaterialItem = thisItem;
    }
}