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
        if (!menuUI.activeInHierarchy)
        {
            return;
        }
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected == null)
        {
            return;
        }
        cursor.Focus(currentSelected.transform.position);
        if (_inputSetting.GetForwardKeyDown() || _inputSetting.GetBackKeyDown() || _inputSetting.GetLeftKeyDown() || _inputSetting.GetRightKeyDown() || _inputSetting.GetMenuKeyDown())
        {
            OnFocused(currentSelected);
        }
        else if (_inputSetting.GetDecideKeyDown())
        {
            OnFocused(currentSelected);
            IPushedObject pushedObject = currentSelected.GetComponent<IPushedObject>();
            pushedObject.OnDecideKeyDown();
        }
        else if (_inputSetting.GetCancelKeyDown())
        {
            OnFocused(currentSelected);
            IPushedObject pushedObject = currentSelected.GetComponent<IPushedObject>();
            pushedObject.OnCancelKeyDown();
        }


    }
    private void OnFocused(GameObject currentSelected)
    {
        if (currentSelected.TryGetComponent<IFocusedObject>(out var focusedObject))
        {
            focusedObject.OnFocused();
        }
    }
}
