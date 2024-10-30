using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image[] images;
    Dictionary<string, Image> imagesDict = new Dictionary<string, Image>();
    Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();
    
    public void Initialize()
    {
        if (!imagesDict.Any())
        {
            for (int i = 0; i < images.Length; i++)
            {
                imagesDict.Add(images[i].name, images[i]);
            }
            for (int i = 0; i < sprites.Length; i++)
            {
                spritesDict.Add(sprites[i].name, sprites[i]);
            }
        }
        
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = null;
            images[i].color = Color.clear;
        }
    }

    public void ChangeImages(ChangeImage[] changeImage)
    {
        // 各スプライトと画像の名前を初期化
        for (int i = 0; i < changeImage.Length; i++)
        {
            //テキストから指定されたImage,Spriteの名前とインスペクターで設定したImage,Spriteの名前が一致したとき
            if (imagesDict.ContainsKey(changeImage[i].ImageName) && spritesDict.ContainsKey(changeImage[i].SpriteName))
            {
                imagesDict[changeImage[i].ImageName].sprite = spritesDict[changeImage[i].SpriteName];
                Color color;
                ColorUtility.TryParseHtmlString("#FFFFFF80", out color);
                imagesDict[changeImage[i].ImageName].color = color;
            }
        }
    }
}

