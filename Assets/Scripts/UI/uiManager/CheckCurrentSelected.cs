using UnityEngine;
using UnityEngine.EventSystems;

public class CheckCurrentSelected : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private Cursor cursor;

    void Start()
    {
        _inputSetting = InputSetting.Load();
    }

    void Update()
    {
        if (menuUI.activeInHierarchy)
        {
            GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
            cursor.Focus(currentSelected.transform.position);
            if (_inputSetting.GetDecideKeyDown())
            {
                IFocusObject focusedObject = currentSelected.GetComponent<IFocusObject>();
                Debug.Log("currentSelected.name : " + currentSelected.name);
                focusedObject.OnDecideKeyDown();
            }
            else if (_inputSetting.GetCancelKeyDown())
            {
                IFocusObject focusedObject = currentSelected.GetComponent<IFocusObject>();
                focusedObject.OnCancelKeyDown();
            }
        }
    }
}
