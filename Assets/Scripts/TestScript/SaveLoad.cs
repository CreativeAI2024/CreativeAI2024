using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MessagePack;
using System.Collections.Generic;

public class SaveLoad : MonoBehaviour
{
    public string saveFilePath = "Assets/Scripts/TestScript/savefile.json"; // �ۑ�����JSON�t�@�C���̃p�X�A���ꂪexample.json�̓��e�ɂȂ��Ă���΂���
    public string exampleFilePath = "Assets/Scripts/TestScript/example.json";

    // UI�v�f
    public Button saveButton;
    public Button loadButton;
    public MyClass myClass; // �f�[�^��ۑ����邽�߂�MyClass�C���X�^���X

    void Start()
    {
        // myClass��������
        //myClass = new MyClass { Id = 1, Name = "Example", Items = new List<string> { "Item1", "Item2", "Item3" } };
        // JSON�t�@�C������f�[�^��ǂݍ���
        myClass = LoadFromJson(exampleFilePath); // example.json �̓��e��ǂݍ���

        // �{�^���̃��X�i�[��ݒ�
        saveButton.onClick.AddListener(() => SaveAsJson(myClass));
        loadButton.onClick.AddListener(() => {
            MyClass loadedData = LoadFromJson(saveFilePath);
            // loadedData���g�p���āAUI�Ȃǂɕ\�����鏈����ǉ�
            if (loadedData != null)
            {
                Debug.Log($"Loaded Data - Id: {loadedData.Id}, Name: {loadedData.Name}, Items: {string.Join(", ", loadedData.Items)}");
            }
        });
    }


    // �N���X��JSON�`���ŕۑ����郁�\�b�h
    public void SaveAsJson(MyClass myClass)
    {
        // MyClass��MessagePack�`���ɃV���A���C�Y
        byte[] msgPackData = MessagePackSerializer.Serialize(myClass);

        // MessagePack�f�[�^��JSON�`���ɕϊ�
        string jsonString = MessagePackSerializer.ConvertToJson(msgPackData);

        // JSON�f�[�^���t�@�C���ɕۑ�
        File.WriteAllText(saveFilePath, jsonString);
        Debug.Log("Data saved as JSON: " + saveFilePath);
    }

    // JSON�`���̃t�@�C����ǂݍ��ރ��\�b�h
    public MyClass LoadFromJson(string filePath)
    {
        // �t�@�C�������݂��邩�m�F
        if (!File.Exists(filePath))
        {
            Debug.LogError("JSON file not found: " + filePath);
            return null;
        }

        // �t�@�C������JSON�f�[�^��ǂݍ���
        //string jsonContent = File.ReadAllText(saveFilePath);
        string jsonContent = File.ReadAllText(filePath);

        // JSON�f�[�^��MessagePack�`���ɕϊ����AMyClass�C���X�^���X�Ƀf�V���A���C�Y
        byte[] msgPackData = MessagePackSerializer.ConvertFromJson(jsonContent);
        MyClass myClass = MessagePackSerializer.Deserialize<MyClass>(msgPackData);

        Debug.Log("Data loaded from JSON.");
        return myClass;
    }
}
