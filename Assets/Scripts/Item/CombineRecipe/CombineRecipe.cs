using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="CombineRecipe", menuName ="ScriptableObject/Item/CombineRecipe")]
public class CombineRecipe : ScriptableObject
{
  [SerializeField] private Item material1;
  [SerializeField] private Item material2;
  [SerializeField] private Item createdItem;
  public Item Material1 => material1;
  public Item Material2 => material2;
  public Item CreatedItem => createdItem;
}
