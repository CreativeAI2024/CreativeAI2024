using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MessagePack;


public class SaveLoad : MonoBehaviour
{
    public string saveFilePath = "Assets/Scripts/TestScript/savefile.json"; // MessagePack�`���̃t�@�C���p�X
    public string jsonFilePath = "Assets/Scripts/TestScript/example.json"; // JSON�`���̃t�@�C���p�X

    // UI�v�f
    public Button saveButton;
    public Button loadButton;
    public MyClass myClass; // �f�[�^��ۑ����邽�߂�MyClass�C���X�^���X

    void Start()
    {
        // �{�^���̃��X�i�[��ݒ�
        saveButton.onClick.AddListener(() => SaveAsMessagePack(myClass));
        loadButton.onClick.AddListener(() => {
            MyClass loadedData = LoadMessagePack();
            // loadedData���g�p���āAUI�Ȃǂɕ\�����鏈����ǉ�
            if (loadedData != null)
            {
                Debug.Log($"Loaded Data - Id: {loadedData.Id}, Name: {loadedData.Name}, Items: {string.Join(", ", loadedData.Items)}");
            }
        });
    }

    // �N���X��MessagePack�`���ŕۑ����郁�\�b�h
    public void SaveAsMessagePack(MyClass myClass)
    {
        // MyClass��MessagePack�`���ɃV���A���C�Y
        byte[] msgPackData = MessagePackSerializer.Serialize(myClass);
        // MessagePack�f�[�^��savefile.json�ɕۑ�
        File.WriteAllBytes(saveFilePath, msgPackData);
        Debug.Log("Data saved as MessagePack to JSON file: " + saveFilePath);
    }

    // MessagePack�`���̃f�[�^��savefile.json����ǂݍ��ރ��\�b�h
    public MyClass LoadMessagePack()
    {
        // savefile.json��ǂݍ���
        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("Save file not found: " + saveFilePath);
            return null;
        }

        // MessagePack�f�[�^��ǂݍ���
        byte[] msgPackData = File.ReadAllBytes(saveFilePath);
        MyClass myClass = MessagePackSerializer.Deserialize<MyClass>(msgPackData); // MessagePack����MyClass�C���X�^���X�֕ϊ�
        Debug.Log("Data loaded from MessagePack.");
        return myClass;
    }
}
