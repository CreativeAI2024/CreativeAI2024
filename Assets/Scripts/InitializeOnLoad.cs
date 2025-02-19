using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

public class InitializeOnLoad
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        StaticCompositeResolver.Instance.Register(
            MessagePack.Unity.UnityResolver.Instance,
            GeneratedResolver.Instance,
            BuiltinResolver.Instance,
            AttributeFormatterResolver.Instance,
            PrimitiveObjectResolver.Instance,
            StandardResolver.Instance
        );
        
        var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
        MessagePackSerializer.DefaultOptions = option;
    }
}