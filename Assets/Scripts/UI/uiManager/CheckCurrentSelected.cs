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
            IFocusedObject focusedObject = currentSelected.GetComponent<IFocusedObject>();
            focusedObject.OnDecideKeyDown();
        }
    }
}
