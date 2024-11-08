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
    //public MyClass myClass; // �f�[�^��ۑ����邽�߂�MyClass�C���X�^���X

    void Start()
    {
        // myClass��������
        //myClass = new MyClass { Id = 1, Name = "Example", Items = new List<string> { "Item1", "Item2", "Item3" } };
        // JSON�t�@�C������f�[�^��ǂݍ���
        //myClass = LoadFromJson(exampleFilePath); // example.json �̓��e��ǂݍ���

        // �{�^���̃��X�i�[��ݒ�
        saveButton.onClick.AddListener(() => SaveAsJson());
        loadButton.onClick.AddListener(() => {
            ObjectData loadedData = LoadFromJson(saveFilePath);
            // loadedData���g�p���āAUI�Ȃǂɕ\�����鏈����ǉ�
            if (loadedData != null)
            {
                Debug.Log($"Loaded Data - Id: {loadedData.Id}, Name: {loadedData.TriggerType}, Items: {string.Join(", ", loadedData.FlagCondition)}");
            }
        });
    }


    // �N���X��JSON�`���ŕۑ����郁�\�b�h
    public void SaveAsJson()
    {
        ObjectData objectData = new ObjectData();
        objectData.Id = 1;
        objectData.Location = new Location[2];
        objectData.Location[0].MapName = "mapname";
        objectData.Location[0].Position = new Vector2Int(1, 1);
        objectData.Location[1].MapName = "mapname2";
        objectData.Location[1].Position = new Vector2Int(2, 2);
        objectData.EventName = "eventname";
        objectData.TriggerType = 1;
        FlagCondition flagCondition = objectData.FlagCondition;
        flagCondition.Flag = new KeyValuePair<string, bool>[2];
        flagCondition.Flag[0] = new KeyValuePair<string, bool>("key1-1", true);
        flagCondition.Flag[1] = new KeyValuePair<string, bool>("key1-2", true);
        flagCondition.NextFlag = new KeyValuePair<string, bool>[2];
        flagCondition.NextFlag[0] = new KeyValuePair<string, bool>("key2-1", true);
        flagCondition.NextFlag[1] = new KeyValuePair<string, bool>("key2-2", true);
        objectData.FlagCondition = flagCondition;
        
        SaveUtility.DataToJson(objectData, saveFilePath);
        Debug.Log("Data saved as JSON: " + saveFilePath);
    }

    // JSON�`���̃t�@�C����ǂݍ��ރ��\�b�h
    public ObjectData LoadFromJson(string filePath)
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
        ObjectData myClass = MessagePackSerializer.Deserialize<ObjectData>(msgPackData);

        Debug.Log("Data loaded from JSON.");
        return myClass;
    }
}
