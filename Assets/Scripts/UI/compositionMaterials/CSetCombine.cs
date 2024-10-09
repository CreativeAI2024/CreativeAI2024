using UnityEngine;

public class CSetCombine
{
  public CSetCombine(){}
  public void SetOpenWindowEnabled(GameObject gameObject, bool isCombinable)
  {
    gameObject.GetComponent<OpenWindow>().enabled = isCombinable;
  }
  public void SetCombineItemName(GameObject confirmYesButton, string itemName)
  {
    confirmYesButton.GetComponent<CombineItems>().MaterialItemName = itemName;
  }
}