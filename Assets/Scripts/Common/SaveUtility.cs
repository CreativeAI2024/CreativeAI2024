using UnityEngine;
using MessagePack;
using System.IO;

public static class SaveUtility
{
    public static T JsonToData<T>(string filePath) where T : class
    {
        //JSONファイルからデータ読み込み、指定した型にデシリアライズ
        IFileAssetLoader loader = FileAssetLoaderFactory();
        string jsonContent = loader.LoadFileAsset(filePath);
        byte[] msgPackData = MessagePackSerializer.ConvertFromJson(jsonContent);

        return MessagePackSerializer.Deserialize<T>(msgPackData);
    }
    
    public static void DataToJson<T>(T data, string savePath) where T : class
    {
        //データをJSON形式の文字列にシリアライズし、指定したパスに保存
        byte[] msgPackData = MessagePackSerializer.Serialize(data);
        string jsonContent = MessagePackSerializer.ConvertToJson(msgPackData);
        File.WriteAllText(savePath, jsonContent);
    }

    public static T SaveFileToData<T>(string filePath) where T : class
    {
        //指定したファイルパスからデータを読み込み、デシリアライズ
        byte[] msgPackData = File.ReadAllBytes(filePath);

        return MessagePackSerializer.Deserialize<T>(msgPackData);
    }
    
    public static void DataToSaveFile<T>(T data, string savePath) where T : class
    {
        byte[] msgPackData = MessagePackSerializer.Serialize(data);
        File.WriteAllBytes(savePath, msgPackData);
    }
    
    public static IFileAssetLoader FileAssetLoaderFactory()
    {
#if UNITY_WEBGL
        return new ResourcesFileAssetLoader();
#else
        return new StreamingAssetLoader();
#endif
    }
}
