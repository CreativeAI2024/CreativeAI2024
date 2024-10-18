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
        if (_inputSetting.GetDecideKeyDown())
        {
            IDecideCancelObject focusedObject = currentSelected.GetComponent<IDecideCancelObject>();
            focusedObject.OnDecideKeyDown();
        }
        else if (_inputSetting.GetCancelKeyDown())
        {
            IDecideCancelObject focusedObject = currentSelected.GetComponent<IDecideCancelObject>();
            focusedObject.OnCancelKeyDown();
        }
    }
}
