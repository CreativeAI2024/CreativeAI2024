using UnityEngine;
using UnityEngine.UI;

public class CShowImage
{
  private GameObject itemImageScreen;
  public CShowImage(GameObject itemImageScreen)
  {
    this.itemImageScreen = itemImageScreen;
  }
  public void Show(BaseItem imageShowItem, GameObject nextWindow)
  {
    ImageShowItem item = (ImageShowItem)imageShowItem;
    itemImageScreen.GetComponent<Image>().sprite = item.Image;
    itemImageScreen.GetComponent<OpenWindow>().nextWindow = nextWindow;
  }
}