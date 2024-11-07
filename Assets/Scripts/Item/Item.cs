using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName ="Item", menuName ="ScriptableObject/Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField, TextArea] private string descriptionText;
    [SerializeField] private int count = 1;
    [FormerlySerializedAs("image")] [SerializeField] private Sprite sprite;
    [SerializeField] private string contentTextFilePath;
    public string ItemName => itemName;
    public string DescriptionText => descriptionText;
    public int Count => count;
    public Sprite Sprite => sprite;
    public string ContentTextFilePath => contentTextFilePath;

    public bool HasContentText()
    {
        DebugLogger.Log("File.Exists(): "+File.Exists(string.Join('/', Application.streamingAssetsPath, "TalkData", contentTextFilePath + ".json")));
        return File.Exists(string.Join('/', Application.streamingAssetsPath, "TalkData", contentTextFilePath + ".json"));
    }
    public void IncrementCount()
    {
        count++;
    }
    
    public void DecrementCount()
    {
        if (count >= 1)
        {
            count--;
        }
        else
        {
            throw new InvalidOperationException("Count cannot be less than 1");
        }
    }
}
