using UnityEngine;
using UnityEngine.UI;

public class CShowImage
{
  private GameObject itemImageScreen;
  public CShowImage(GameObject itemImageScreen)
  {
    this.itemImageScreen = itemImageScreen;
  }
  public void Show(BaseItem imageShowItem)
  {
    ImageShowItem item = (ImageShowItem)imageShowItem;
    Sprite image = item.Image;
    itemImageScreen.GetComponent<Image>().sprite = image;
    ChangeEnabled(true);

  }
  public void ChangeEnabled(bool IsEnabled)
  {
    itemImageScreen.GetComponent<Image>().enabled = IsEnabled;
  }
}