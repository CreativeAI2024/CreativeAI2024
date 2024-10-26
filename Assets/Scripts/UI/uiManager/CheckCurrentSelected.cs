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
            if (_inputSetting.GetForwardKeyDown() || _inputSetting.GetBackKeyDown() || _inputSetting.GetLeftKeyDown() || _inputSetting.GetRightKeyDown() || _inputSetting.GetDecideKeyDown() || _inputSetting.GetBackKeyDown() || _inputSetting.GetMenuKeyDown())
            {
                if (currentSelected.TryGetComponent<IFocusedObject>(out var focusedObject))
                {
                    focusedObject.OnFocused();
                }
            }
            else if (_inputSetting.GetDecideKeyDown())
            {
                IPushedObject pushedObject = currentSelected.GetComponent<IPushedObject>();
                pushedObject.OnDecideKeyDown();
            }
            else if (_inputSetting.GetCancelKeyDown())
            {
                IPushedObject pushedObject = currentSelected.GetComponent<IPushedObject>();
                pushedObject.OnCancelKeyDown();
            }
        }
    }
}
