using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckCurrentSelected : MonoBehaviour
{
    private InputSetting _inputSetting;
    private GameObject currentSelected;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        currentSelected = EventSystem.current.currentSelectedGameObject;
    }
    void Update()
    {
        if (_inputSetting.GetForwardKeyDown() || _inputSetting.GetBackKeyDown() || _inputSetting.GetLeftKeyDown() || _inputSetting.GetRightKeyDown())
        {
            IFocusedObject focusedObject = currentSelected.GetComponent<IFocusedObject>();
            focusedObject.OnDirectionKeyDown();
        }
        else if (_inputSetting.GetDecideKeyDown())
        {
            IFocusedObject focusedObject = currentSelected.GetComponent<IFocusedObject>();
            focusedObject.OnDecideKeyDown();
        }
        else if (_inputSetting.GetCancelKeyDown())
        {
            IFocusedObject focusedObject = currentSelected.GetComponent<IFocusedObject>();
            focusedObject.OnCancelKeyDown();
        }
    }
}
