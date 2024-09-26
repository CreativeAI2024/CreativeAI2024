using UnityEngine;

public class CSetCombine
{
  public CSetCombine(){}
  public void SetEnabled(GameObject gameObject, bool isCombinable)
  {
    gameObject.GetComponent<OpenWindow>().enabled = isCombinable;
  }
  public void SetItemName(GameObject confirmYesButton, string itemName)
  {
    confirmYesButton.GetComponent<Combine>().ItemName = itemName;
  }
}