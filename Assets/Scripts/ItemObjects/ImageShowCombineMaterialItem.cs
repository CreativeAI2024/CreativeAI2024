using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName ="ImageShowCombineMaterialItem", menuName ="ScriptableObject/Item/ImageShowCombineMaterialItem")]
public class ImageShowCombineMaterialItem : BaseItem, IImageShow, ICombineMaterial
{
    [SerializeField] private Sprite image;
    [SerializeField] private BaseItem pairItem;
    [SerializeField] private BaseItem createdItem;

    public Sprite Image => image;
    public CombineMaterialItem PairItem => (CombineMaterialItem)pairItem;
    public BaseItem CreatedItem => createdItem;
}