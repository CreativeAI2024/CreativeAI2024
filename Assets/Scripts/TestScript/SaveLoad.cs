using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MessagePack;


public class SaveLoad : MonoBehaviour
{
    public string saveFilePath = "Assets/Scripts/TestScript/savefile.json"; // MessagePack形式のファイルパス
    public string jsonFilePath = "Assets/Scripts/TestScript/example.json"; // JSON形式のファイルパス

    // UI要素
    public Button saveButton;
    public Button loadButton;
    public MyClass myClass; // データを保存するためのMyClassインスタンス

    void Start()
    {
        // ボタンのリスナーを設定
        saveButton.onClick.AddListener(() => SaveAsMessagePack(myClass));
        loadButton.onClick.AddListener(() => {
            MyClass loadedData = LoadMessagePack();
            // loadedDataを使用して、UIなどに表示する処理を追加
            if (loadedData != null)
            {
                Debug.Log($"Loaded Data - Id: {loadedData.Id}, Name: {loadedData.Name}, Items: {string.Join(", ", loadedData.Items)}");
            }
        });
    }

    // クラスをMessagePack形式で保存するメソッド
    public void SaveAsMessagePack(MyClass myClass)
    {
        // MyClassをMessagePack形式にシリアライズ
        byte[] msgPackData = MessagePackSerializer.Serialize(myClass);
        // MessagePackデータをsavefile.jsonに保存
        File.WriteAllBytes(saveFilePath, msgPackData);
        Debug.Log("Data saved as MessagePack to JSON file: " + saveFilePath);
    }

    // MessagePack形式のデータをsavefile.jsonから読み込むメソッド
    public MyClass LoadMessagePack()
    {
        // savefile.jsonを読み込み
        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("Save file not found: " + saveFilePath);
            return null;
        }

        // MessagePackデータを読み込む
        byte[] msgPackData = File.ReadAllBytes(saveFilePath);
        MyClass myClass = MessagePackSerializer.Deserialize<MyClass>(msgPackData); // MessagePackからMyClassインスタンスへ変換
        Debug.Log("Data loaded from MessagePack.");
        return myClass;
    }
}
