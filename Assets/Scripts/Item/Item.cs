using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField, TextArea] private string descriptionText;
    [FormerlySerializedAs("image")][SerializeField] private Sprite sprite;
    [SerializeField] private string contentTextFilePath;
    public string ItemName => itemName;
    public string DescriptionText => descriptionText;
    public Sprite Sprite => sprite;
    public string ContentTextFilePath => contentTextFilePath;

    public bool HasContentText()
    {
        return File.Exists(string.Join('/', Application.streamingAssetsPath, "TalkData", contentTextFilePath + ".json"));
    }
}
