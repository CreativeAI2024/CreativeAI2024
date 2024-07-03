using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName ="ImageShowItem", menuName ="ScriptableObject/Item/ImageShowItem")]
public class ImageShowItem : BaseItem
{
    [SerializeField] private Sprite image;
    public Sprite Image => image;
}