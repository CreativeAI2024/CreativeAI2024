using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject windowBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputSetting.GetItemKeyDown())
        {
            ToggleWindow();
        }
    }
    public void ToggleWindow()
    {
        UIUtility.ChangeActive(windowBox, !windowBox.activeSelf);
    }
}
