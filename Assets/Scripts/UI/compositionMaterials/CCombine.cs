public class CCombine
{
  private ItemList itemList;
  public CCombine(ItemList itemList)
  {
    this.itemList = itemList; 
  }

  public bool Combine(string itemName)
  {
    CombineMaterialItem thisItem = (CombineMaterialItem)itemList.Search(itemName);
    if (itemList.Search(thisItem.PairItem.ItemName)!=null)
    {
      itemList.Add(thisItem.CreatedItem.ItemName);
      itemList.Remove(itemName);
      itemList.Remove(thisItem.PairItem.ItemName);
      return true;
    } else {
      return false;
    }
  }
}