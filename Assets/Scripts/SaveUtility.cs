using UnityEngine;
using MessagePack;
using System.IO;

public static class SaveUtility
{
    public static T JsonToData<T>(string filePath) where T : class
    {
        //JSONファイルからデータ読み込み、指定した型にデシリアライズ
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("JSON file not found: " + filePath);
        }

        string jsonContent = File.ReadAllText(filePath);
        byte[] msgPackData = MessagePackSerializer.ConvertFromJson(jsonContent);
        T data = MessagePackSerializer.Deserialize<T>(msgPackData);

        return data;
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
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Save file not found: " + filePath);
        }

        byte[] msgPackData = File.ReadAllBytes(filePath);
        T data = MessagePackSerializer.Deserialize<T>(msgPackData);

        return data;
    }
    
    public static void DataToSaveFile<T>(T data, string savePath) where T : class
    {
        string jsonContent = JsonUtility.ToJson(data);
        File.WriteAllText($"{savePath}", jsonContent);
        byte[] msgPackData = MessagePackSerializer.Serialize(data);
        File.WriteAllBytes(savePath, msgPackData);
    }
}
