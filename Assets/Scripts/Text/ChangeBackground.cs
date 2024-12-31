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

    public void ChangeImages(ChangeImage[] changeImages)
    {
        // 各スプライトと画像の名前を初期化
        //for (int i = 0; i < changeImage.Length; i++)
        foreach(var changeImage in changeImages) 
        {
            string imageName = changeImage.ImageName;
            string spriteName = changeImage.SpriteName;
            //テキストから指定されたImage,Spriteの名前とインスペクターで設定したImage,Spriteの名前が一致したとき
            if (imagesDict.ContainsKey(imageName))
            {
                if (spriteName.StartsWith("Rei"))  //statusSpriteAndFlags.Keysに含まれる名称のとき
                {
                    ChangeSprite(imageName, $"{spriteName}_0");
                    // ChangeSprite(imageName, $"{spriteName}_{FlagManager.Instance.ReiStatus}");
                }else if (spritesDict.ContainsKey(spriteName))
                {
                    ChangeSprite(imageName, spriteName);
                }

            }
        }
    }

    private void ChangeSprite(string image,string sprite)
    {
        imagesDict[image].sprite = spritesDict[sprite];
        ColorUtility.TryParseHtmlString("#FFFFFFFF", out Color color);
        imagesDict[image].color = color;
    }
}

