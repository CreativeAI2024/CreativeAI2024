using MessagePack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonAttach : MonoBehaviour
{
    public TextAsset jsonFile;
    private TalkData deserializedData;

    public void LoadJson()
    {
        byte[] messagePackData = MessagePackSerializer.ConvertFromJson(jsonFile.text);

        deserializedData = MessagePackSerializer.Deserialize<TalkData>(messagePackData);
    }

    public Content GetContent(int index)
    {
        return deserializedData.Content[index];
    }

    public int GetTextLines()
    {
        return deserializedData.Content.Length;
    }
}
