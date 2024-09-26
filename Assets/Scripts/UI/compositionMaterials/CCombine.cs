using UnityEngine;
using UnityEngine.UI;

public class CCombine
{
  private ItemList itemList;
  private CombineMaterialItem thisItem;
  private string thisItemName;
  private string pairItemName;
  public CCombine(ItemList itemList, string itemName)
  {
    this.itemList = itemList; 
    thisItemName = itemName;
    thisItem = (CombineMaterialItem)itemList.Search(thisItemName);
    pairItemName = thisItem.PairItem.ItemName;
  }

  public bool Combine()
  {
    Debug.Log("Combine ran");
    if (itemList.Search(pairItemName)!=null)
    {
      Debug.Log("Pair Item Found");
      itemList.Add(thisItem.CreatedItem.ItemName);
      itemList.Remove(thisItemName);
      itemList.Remove(pairItemName);
      return true;
    } else {
      return false;
    }
  }
}