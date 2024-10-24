using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image[] images;
    Dictionary<string, Image> imagesDict = new Dictionary<string, Image>();
    Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();

    private void Start()
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

    /*public void ChangeImages(string words)
    {
        TextConverter textConverter = new TextConverter();
        string[][] spriteAndImage = textConverter.Converter(words);  //spriteAndImage[0]: spriteNameの配列, spriteAndImage[1]: imageNameの配列

        // 各スプライトと画像の名前を初期化
        for (int i = 0; i < spriteAndImage[0].Length; i++)
        {
            //テキストから指定されたImage,Spriteの名前とインスペクターで設定したImage,Spriteの名前が一致したとき
            if (imagesDict.ContainsKey(spriteAndImage[1][i]) && spritesDict.ContainsKey(spriteAndImage[0][i]))
            {
                imagesDict[spriteAndImage[1][i]].sprite = spritesDict[spriteAndImage[0][i]];
            }
        }
    }*/

    public void ChangeImages(ChangeImage[] changeImage)
    {
        //TextConverter textConverter = new TextConverter();
        //string[][] spriteAndImage = textConverter.Converter(words);  //spriteAndImage[0]: spriteNameの配列, spriteAndImage[1]: imageNameの配列

        // 各スプライトと画像の名前を初期化
        for (int i = 0; i < changeImage.Length; i++)
        {
            //テキストから指定されたImage,Spriteの名前とインスペクターで設定したImage,Spriteの名前が一致したとき
            if (imagesDict.ContainsKey(changeImage[i].Image) && spritesDict.ContainsKey(changeImage[i].Sprite))
            {
                imagesDict[changeImage[i].Image].sprite = spritesDict[changeImage[i].Sprite];
            }
        }
    }
}

