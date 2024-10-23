using UnityEngine;
using MessagePack;
using System.IO;

public static class SaveUtility
{
    public static T JsonToData<T>(string filePath) where T : class
    {
        //JSON�t�@�C������f�[�^�ǂݍ��݁A�w�肵���^�Ƀf�V���A���C�Y
        string jsonContent = File.ReadAllText(filePath);
        byte[] msgPackData = MessagePackSerializer.ConvertFromJson(jsonContent);

        return MessagePackSerializer.Deserialize<T>(msgPackData);
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
        byte[] msgPackData = File.ReadAllBytes(filePath);

        return MessagePackSerializer.Deserialize<T>(msgPackData);
    }
    
    public static void DataToSaveFile<T>(T data, string savePath) where T : class
    {
        byte[] msgPackData = MessagePackSerializer.Serialize(data);
        File.WriteAllBytes(savePath, msgPackData);
    }
}
