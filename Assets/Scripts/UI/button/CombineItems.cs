using UnityEngine;
using UnityEngine.EventSystems;

public class CombineItems : MonoBehaviour
{
    private InputSetting _inputSetting;
    private ItemList itemList;
    private CombineRecipeDatabase combineRecipeDatabase;
    private string materialItemName;
    public string MaterialItemName {
        set {materialItemName = value;}
    }
    void Start()
    {
        _inputSetting = InputSetting.Load();
        itemList = Resources.Load<ItemList>("Items/ItemList");
        combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");
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
    string pairItemName = combineRecipeDatabase.GetPairItem(materialItemName).ItemName; //error
    if (itemList.Search(pairItemName)!=null)
    {
      itemList.Add(combineRecipeDatabase.GetCreatedItem(materialItemName).ItemName);
      itemList.Remove(materialItemName);
      itemList.Remove(pairItemName);
    } 
  }
}