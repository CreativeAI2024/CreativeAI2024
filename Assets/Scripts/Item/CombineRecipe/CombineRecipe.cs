using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="CombineRecipe", menuName ="ScriptableObject/Item/CombineRecipe")]
public class CombineRecipe : ScriptableObject
{
  [SerializeField] private BaseItem material1;
  [SerializeField] private BaseItem material2;
  [SerializeField] private BaseItem createdItem;
  public BaseItem Material1 => material1;
  public BaseItem Material2 => material2;
  public BaseItem CreatedItem => createdItem;
}
