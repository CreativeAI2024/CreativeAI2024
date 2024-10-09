using UnityEngine;
using UnityEngine.EventSystems;

public class CombineItems : MonoBehaviour
{
    private InputSetting _inputSetting;
    private ItemList itemList;
    private CombineRecipeList combineRecipeList;
    private string materialItemName;
    public string MaterialItemName {
        set {materialItemName = value;}
    }
    void Start()
    {
        _inputSetting = InputSetting.Load();
        itemList = Resources.Load<ItemList>("Items/ItemList");
        combineRecipeList = Resources.Load<CombineRecipeList>("Items/CombineRecipes/CombineRecipeList");
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (_inputSetting.GetDecideKeyDown())
            {
                Combine();
            }
        }
    }

    private void Combine()
  {
    Debug.Log("Combine() called");
    string pairItemName = combineRecipeList.GetPairItem(materialItemName).ItemName; //error
    if (itemList.Search(pairItemName)!=null)
    {
      itemList.Add(combineRecipeList.GetCreatedItem(materialItemName).ItemName);
      itemList.Remove(materialItemName);
      itemList.Remove(pairItemName);
    } 
  }
}