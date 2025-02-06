#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image[] images;
    [SerializeField] private Sprite voidImage;
    Dictionary<string, Image> imagesDict = new Dictionary<string, Image>();
    Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();
    private float speakerBrightness = 1;
    private float listenerBrightness = 0.5f;
    private Dictionary<string, string> speakerToSpriteDict = new Dictionary<string, string>
    {
        {"レイ", "Rei"},
        {"ヒカル", "Hikaru"},
        {"アザミ", "Azami"},
        {"？？？", "Azami"},
        {"テッセン", "Tessen"}
    };

    public void Initialize()
    {
        if (!imagesDict.Any())
        {
            for (int i = 0; i < images.Length; i++)
            {
                imagesDict.Add(images[i].name, images[i]);
                if (images[i].name.Equals("BackgroundPanel")) continue;
                ChangeBrightness(images[i].name, listenerBrightness);
            }
            for (int i = 0; i < sprites.Length; i++)
            {
                spritesDict.Add(sprites[i].name, sprites[i]);
            }
        }
        
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = voidImage;
            images[i].color = Color.clear;
        }
    }

    public void ChangeImages(ChangeImage[] changeImages)
    {
        // 各スプライトと画像の名前を初期化
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
        UnityEngine.ColorUtility.TryParseHtmlString("#FFFFFFFF", out Color color);
        imagesDict[image].color = color;
    }

    public void HighlightSpeakerSprite(string speaker)
    {
        foreach(Image image in images)
        {
            string imageName = image.name;
            string spriteName = image.sprite.name;
            if (imageName.Equals("BackgroundPanel")) continue;
            ChangeBrightness(imageName, SelectBrightness(speaker, spriteName));
        }
    }

    private float SelectBrightness(string speaker, string spriteName)
    {
        return (speakerToSpriteDict.ContainsKey(speaker) && spriteName.Contains(speakerToSpriteDict[speaker])) ? speakerBrightness : listenerBrightness;
    }
    private void ChangeBrightness(string imageName, float brightness)
    {
        Color currentColor = imagesDict[imageName].color;
        Color.RGBToHSV(currentColor, out float h, out float s, out _);
        Color newColor = Color.HSVToRGB(h, s, brightness);
        imagesDict[imageName].color = newColor;
    }
}

