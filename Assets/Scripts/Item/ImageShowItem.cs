using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName ="ImageShowItem", menuName ="ScriptableObject/Item/ImageShowItem")]
public class ImageShowItem : BaseItem, IImageShow
{
    [SerializeField] private Sprite image;
    public Sprite Image => image;
}