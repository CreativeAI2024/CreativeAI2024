using TMPro;
using UnityEngine;
public class CheckCombinable : MonoBehaviour
{
  [SerializeField] private ItemList itemList;
  [SerializeField] private GameObject itemImageScreen;
  [SerializeField] private GameObject confirmWindow;
  void OnEnable()
  {
    Check();
  }
  void Start()
  {
    Check();
  }

  private void Check()
  {
    foreach (Transform child in transform)
    {
      switch (itemList.Search(child.GetChild(0).GetComponent<TextMeshProUGUI>().text))
      {
        case ImageShowCombineMaterialItem item:
          if (itemList.Search(item.PairItem.ItemName) == true)
          {
            ImageShowCombineMaterialChangeEnabled(child, true);
          }
          else
          {
            ImageShowCombineMaterialChangeEnabled(child, false);
          }
          break;
        case ImageShowItem:
          break;
        case CombineMaterialItem item:
          if (itemList.Search(item.PairItem.ItemName) == true)
          {
            CombineMaterialChangeEnabled(child, true);
          }
          else
          {
            CombineMaterialChangeEnabled(child, false);
          }
          break;
      }
    }
  }

  private void CombineMaterialChangeEnabled(Transform child, bool isCombinable)
  {
    child.GetComponent<OpenWindow>().enabled = isCombinable;
  }

  private void ImageShowCombineMaterialChangeEnabled(Transform child, bool isCombinable)
  {
    if(isCombinable)
    {
      itemImageScreen.GetComponent<OpenWindow>().nextWindow = confirmWindow;
    }
    else 
    {
      itemImageScreen.GetComponent<OpenWindow>().nextWindow = transform.parent.gameObject;
    }
  }
}