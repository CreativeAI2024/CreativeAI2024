using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName ="CombineMaterialItem", menuName ="ScriptableObject/Item/CombineMaterialItem")]
public class CombineMaterialItem : BaseItem, ICombineMaterial
{
    [SerializeField] private CombineMaterialItem pairItem;
    [SerializeField] private BaseItem createdItem;

    public CombineMaterialItem PairItem => pairItem;
    public BaseItem CreatedItem => createdItem;
}