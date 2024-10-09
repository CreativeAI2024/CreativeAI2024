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
        string jsonContent = File.ReadAllText(filePath); // JSON�t�@�C���̓ǂݍ���

        // JSON��C#�I�u�W�F�N�g�Ƀf�V���A���C�Y
        var myObject = JsonUtility.FromJson<MyClass>(jsonContent);

        // MessagePack�̃V���A���C�Y�I�v�V�������w��
        var options = MessagePackSerializerOptions.Standard; // �����ǉ�

        // MyClass�I�u�W�F�N�g��MessagePack�ŃV���A���C�Y
        byte[] serializedData = MessagePackSerializer.Serialize(myObject, options); // �I�v�V������ǉ�

        // MessagePack�f�[�^���f�V���A���C�Y
        var deserializedObject = MessagePackSerializer.Deserialize<MyClass>(serializedData, options); // �I�v�V������ǉ�

        // �f�V���A���C�Y���ʂ�\��
        Debug.Log($"Id: {deserializedObject.Id}, Name: {deserializedObject.Name}, Items: {string.Join(", ", deserializedObject.Items)}");
    }
}
