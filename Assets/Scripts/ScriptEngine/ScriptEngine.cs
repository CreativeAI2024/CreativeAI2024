using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEngine : MonoBehaviour
{
    [SerializeField] private MapEvent mapEvent;
    
    [SerializeField] private ObjectEngine objectEngine;
    [SerializeField] private MapEngine mapEngine;
    // Start is called before the first frame update
    void Start()
    {
        mapEngine.Initialize();
        int width = mapEngine.Width;
        int height = mapEngine.Height;
        objectEngine.Initialize(width, height);
        
    }
}
