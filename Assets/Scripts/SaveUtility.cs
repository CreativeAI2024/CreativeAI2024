using UnityEngine;
using MessagePack;
using System.IO;

public static class SaveUtility
{
    public static T JsonToData<T>(string filePath) where T : class
    {
        //JSON�t�@�C������f�[�^�ǂݍ��݁A�w�肵���^�Ƀf�V���A���C�Y
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
        //�f�[�^��JSON�`���̕�����ɃV���A���C�Y���A�w�肵���p�X�ɕۑ�
        byte[] msgPackData = MessagePackSerializer.Serialize(data);
        string jsonContent = MessagePackSerializer.ConvertToJson(msgPackData);
        File.WriteAllText(savePath, jsonContent);
    }

    public static T SaveFileToData<T>(string filePath) where T : class
    {
        //�w�肵���t�@�C���p�X����f�[�^��ǂݍ��݁A�f�V���A���C�Y
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
