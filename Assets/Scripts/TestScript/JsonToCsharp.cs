using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MessagePack;
using MessagePack.Resolvers;

[MessagePackObject]
public class MyClass
{
    [Key(0)]
    public int Id { get; set; }

    [Key(1)]
    public string Name { get; set; }

    [Key(2)]
    public List<string> Items { get; set; }
}

public class JsonToCsharp : MonoBehaviour
{
    void Start()
    {
        string filePath = Application.dataPath + "/Scripts/TestScript/example.json";
        string jsonContent = File.ReadAllText(filePath); // JSONファイルの読み込み

        // JSONをC#オブジェクトにデシリアライズ
        var myObject = JsonUtility.FromJson<MyClass>(jsonContent);

        // MessagePackのシリアライズオプションを指定
        var options = MessagePackSerializerOptions.Standard; // これを追加

        // MyClassオブジェクトをMessagePackでシリアライズ
        byte[] serializedData = MessagePackSerializer.Serialize(myObject, options); // オプションを追加

        // MessagePackデータをデシリアライズ
        var deserializedObject = MessagePackSerializer.Deserialize<MyClass>(serializedData, options); // オプションを追加

        // デシリアライズ結果を表示
        Debug.Log($"Id: {deserializedObject.Id}, Name: {deserializedObject.Name}, Items: {string.Join(", ", deserializedObject.Items)}");
    }
}
