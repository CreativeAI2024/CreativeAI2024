using UnityEngine;
using UnityEngine.EventSystems;

public class CheckCurrentSelected : MonoBehaviour
{
    private InputSetting _inputSetting;
    
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    
    void Update()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (_inputSetting.GetDecideKeyDown())
        {
            IFocusObject focusedObject = currentSelected.GetComponent<IFocusObject>();
            Debug.Log("currentSelected.name : "+currentSelected.name);
            focusedObject.OnDecideKeyDown();
        }
        else if (_inputSetting.GetCancelKeyDown())
        {
            IFocusObject focusedObject = currentSelected.GetComponent<IFocusObject>();
            focusedObject.OnCancelKeyDown();
        }
    }
}
