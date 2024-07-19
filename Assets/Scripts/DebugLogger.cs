using System.Diagnostics;

public static class DebugLogger
{
    private static readonly string[] colorCodes = new string[]
    {
        "#ffffff",
        "#ff0000",
        "#ff00ff",
        "#0000ff",
        "#00ffff",
        "#00ff00",
        "#ffff00"
    };
    
    public enum Colors
    {
        White,
        Red,
        Magenta,
        Blue,
        Cyan,
        Green,
        Yellow
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void Log(this object o)
    {
        Log(o, Colors.White);
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void Log(this object o, Colors colorLabel)
    {
        UnityEngine.Debug.Log($"<size=24><color={colorCodes[(int)colorLabel]}>#</color></size>({o.GetType()}) : {o}");
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void Log(string tabMessage)
    {
        Log(tabMessage, Colors.White);
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void Log(string tabMessage, Colors colorLabel)
    {
        UnityEngine.Debug.Log($"<size=24><color={colorCodes[(int)colorLabel]}>#</color></size>{tabMessage}");
    }
}