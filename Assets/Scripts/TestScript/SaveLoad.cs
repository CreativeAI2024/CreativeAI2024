using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MessagePack;
using System.Collections.Generic;

public class SaveLoad : MonoBehaviour
{
    public string saveFilePath = "Assets/Scripts/TestScript/savefile.json"; // 保存するJSONファイルのパス、これがexample.jsonの内容になっていればいい
    public string exampleFilePath = "Assets/Scripts/TestScript/example.json";

    // UI要素
    public Button saveButton;
    public Button loadButton;
    public MyClass myClass; // データを保存するためのMyClassインスタンス

    void Start()
    {
        // myClassを初期化
        //myClass = new MyClass { Id = 1, Name = "Example", Items = new List<string> { "Item1", "Item2", "Item3" } };
        // JSONファイルからデータを読み込む
        myClass = LoadFromJson(exampleFilePath); // example.json の内容を読み込む

        // ボタンのリスナーを設定
        saveButton.onClick.AddListener(() => SaveAsJson(myClass));
        loadButton.onClick.AddListener(() => {
            MyClass loadedData = LoadFromJson(saveFilePath);
            // loadedDataを使用して、UIなどに表示する処理を追加
            if (loadedData != null)
            {
                Debug.Log($"Loaded Data - Id: {loadedData.Id}, Name: {loadedData.Name}, Items: {string.Join(", ", loadedData.Items)}");
            }
        });
    }


    // クラスをJSON形式で保存するメソッド
    public void SaveAsJson(MyClass myClass)
    {
        // MyClassをMessagePack形式にシリアライズ
        byte[] msgPackData = MessagePackSerializer.Serialize(myClass);

        // MessagePackデータをJSON形式に変換
        string jsonString = MessagePackSerializer.ConvertToJson(msgPackData);

        // JSONデータをファイルに保存
        File.WriteAllText(saveFilePath, jsonString);
        Debug.Log("Data saved as JSON: " + saveFilePath);
    }

    // JSON形式のファイルを読み込むメソッド
    public MyClass LoadFromJson(string filePath)
    {
        // ファイルが存在するか確認
        if (!File.Exists(filePath))
        {
            Debug.LogError("JSON file not found: " + filePath);
            return null;
        }

        // ファイルからJSONデータを読み込み
        //string jsonContent = File.ReadAllText(saveFilePath);
        string jsonContent = File.ReadAllText(filePath);

        // JSONデータをMessagePack形式に変換し、MyClassインスタンスにデシリアライズ
        byte[] msgPackData = MessagePackSerializer.ConvertFromJson(jsonContent);
        MyClass myClass = MessagePackSerializer.Deserialize<MyClass>(msgPackData);

        Debug.Log("Data loaded from JSON.");
        return myClass;
    }
}
