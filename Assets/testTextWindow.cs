using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTextWindow : MonoBehaviour
{
    [SerializeField] private string name;
    private InputSetting inputSetting;
    // Start is called before the first frame update
    void Start()
    {
        inputSetting = InputSetting.Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputSetting.GetDecideInputDown())
        {
            onClick();
        }
    }
    public void onClick()
    {
        DebugLogger.Log("clicked");
        ConversationTextManager.Instance.InitializeFromJson(name);
    }
}
