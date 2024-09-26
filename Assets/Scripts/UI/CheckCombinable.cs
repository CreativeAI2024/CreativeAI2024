using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
public class CheckCombinable : MonoBehaviour
{
  [SerializeField] private ItemList itemList;
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
        case CombineMaterialItem item:
          if (itemList.Search(item.PairItem.ItemName)==true)
          {
            ChangeEnabled(child, true);
          }
          else
          {
            ChangeEnabled(child, false);
          }
        break;
      }
    }
  }

  private void ChangeEnabled(Transform child, bool isEnabled)
  {
    child.GetComponent<OpenWindow>().enabled = isEnabled;
  }
}