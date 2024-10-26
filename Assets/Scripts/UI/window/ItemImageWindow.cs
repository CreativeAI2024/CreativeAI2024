using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageWindow : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
