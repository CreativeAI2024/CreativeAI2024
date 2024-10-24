using MessagePack;
using Newtonsoft.Json;
using MessagePack.Resolvers;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class JsonAttach : MonoBehaviour
{
    public TextAsset jsonFile;
    private TextWindowClass deserializedData;


    void Start()
    {
        //var deserializedObject = JsonConvert.DeserializeObject<TextWindowClass>(jsonFile.text);

        //var options = MessagePackSerializerOptions.Standard;


        byte[] messagePackData = MessagePackSerializer.ConvertFromJson(jsonFile.text);

        deserializedData = MessagePackSerializer.Deserialize<TextWindowClass>(messagePackData);
    }

    public Content GetContent(int index)
    {
        return deserializedData.Content[index];
    }
}
