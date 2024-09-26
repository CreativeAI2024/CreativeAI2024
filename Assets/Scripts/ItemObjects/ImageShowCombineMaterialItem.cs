using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName ="ImageShowCombineMaterialItem", menuName ="ScriptableObject/Item/ImageShowCombineMaterialItem")]
public class ImageShowCombineMaterialItem : CombineMaterialItem, IImageShow, ICombineMaterial
{
    [SerializeField] private Sprite image;

    public Sprite Image => image;
}