using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName ="CombineMaterialItem", menuName ="ScriptableObject/Item/CombineMaterialItem")]
public class CombineMaterialItem : BaseItem, ICombineMaterial
{
    [SerializeField] private BaseItem pairItem;
    [SerializeField] private BaseItem createdItem;

    public CombineMaterialItem PairItem => (CombineMaterialItem)pairItem;
    public BaseItem CreatedItem => createdItem;
}