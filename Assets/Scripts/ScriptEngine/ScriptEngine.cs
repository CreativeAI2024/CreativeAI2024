using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEngine : MonoBehaviour
{
    [SerializeField] private ObjectEngine objectEngine;
    [SerializeField] private MapEngine mapEngine;
    [SerializeField] private MapDataController mapDataController;
    
    [SerializeField] private string mapName;
    // Start is called before the first frame update
    void Start()
    {
        mapDataController.LoadMapData(mapName);
        mapEngine.Initialize();
        int width = mapDataController.GetMapSize().x;
        int height = mapDataController.GetMapSize().y;
        objectEngine.Initialize(mapName, width, height);
    }
}
