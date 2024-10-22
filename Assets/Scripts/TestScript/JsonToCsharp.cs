using UnityEngine;
using MessagePack;
using System.IO;

public class JsonToCsharp : MonoBehaviour
{
    public SaveLoad saveLoadSystem; // SaveLoadSystemの参照

    void Start()
    {
        // JSONファイルのパスを指定
        string filePath = Application.dataPath + "/Scripts/TestScript/example.json";

        // JSONファイルの内容を読み込み
        if (!File.Exists(filePath))
        {
            Debug.LogError("JSON file not found: " + filePath);
            return;
        }

        // jsonからクラス
        string jsonContent = File.ReadAllText(filePath);
        Debug.Log("JSON Content: " + jsonContent);

        try
        {
            byte[] msgPackData = MessagePackSerializer.ConvertFromJson(jsonContent);
            MyClass myClass = MessagePackSerializer.Deserialize<MyClass>(msgPackData);

            // 必要に応じて、myClassを保存する
            saveLoadSystem.SaveAsMessagePack(myClass);
            Debug.Log("Data converted and saved as MessagePack.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to convert JSON to MyClass: " + e.Message);
        }
    }
}
