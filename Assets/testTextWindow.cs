using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class testTextWindow : MonoBehaviour
{
    [SerializeField] private string name;
    private InputSetting inputSetting;
    // Start is called before the first frame update
    void Start()
    {
        DebugLogger.Log(Application.persistentDataPath);
        inputSetting = InputSetting.Load();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }
    public void onClick()
    {
        ConversationTextManager.Instance.InitializeFromJson(name);
    }
}
