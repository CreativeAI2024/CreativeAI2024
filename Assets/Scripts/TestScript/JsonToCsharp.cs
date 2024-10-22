using UnityEngine;
using MessagePack;
using System.IO;

public class JsonToCsharp : MonoBehaviour
{
    public SaveLoad saveLoadSystem; // SaveLoadSystem�̎Q��

    void Start()
    {
        // JSON�t�@�C���̃p�X���w��
        string filePath = Application.dataPath + "/Scripts/TestScript/example.json";

        // JSON�t�@�C���̓��e��ǂݍ���
        if (!File.Exists(filePath))
        {
            Debug.LogError("JSON file not found: " + filePath);
            return;
        }

        // json����N���X
        string jsonContent = File.ReadAllText(filePath);
        Debug.Log("JSON Content: " + jsonContent);

        try
        {
            byte[] msgPackData = MessagePackSerializer.ConvertFromJson(jsonContent);
            MyClass myClass = MessagePackSerializer.Deserialize<MyClass>(msgPackData);

            // �K�v�ɉ����āAmyClass��ۑ�����
            saveLoadSystem.SaveAsMessagePack(myClass);
            Debug.Log("Data converted and saved as MessagePack.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to convert JSON to MyClass: " + e.Message);
        }
    }
}
