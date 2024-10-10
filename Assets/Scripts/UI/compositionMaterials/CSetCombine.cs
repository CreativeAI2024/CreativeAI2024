using UnityEngine;

public class CSetCombine
{
  public CSetCombine(){}
  public void SetOpenWindowEnabled(GameObject gameObject, bool isCombinable)
  {
    gameObject.GetComponent<OpenWindow>().enabled = isCombinable;
  }
  public void SetCombineItem(GameObject confirmYesButton, BaseItem item)
  {
    confirmYesButton.GetComponent<CombineItems>().MaterialItem = item;
  }
}