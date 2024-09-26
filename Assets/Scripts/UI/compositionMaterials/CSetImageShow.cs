using UnityEngine;
using UnityEngine.UI;

public class CSetImageShow
{
  private GameObject itemImageScreen;
  public CSetImageShow(GameObject itemImageScreen)
  {
    this.itemImageScreen = itemImageScreen;
  }
  public void SetNextWindow(GameObject nextWindow)
  {
    itemImageScreen.GetComponent<OpenWindow>().nextWindow = nextWindow;
  }
  public void SetImage(Sprite itemImage)
  {
    itemImageScreen.GetComponent<Image>().sprite = itemImage;
  }
}