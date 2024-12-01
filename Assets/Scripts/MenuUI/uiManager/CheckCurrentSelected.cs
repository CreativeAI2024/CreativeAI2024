using UnityEngine;
using UnityEngine.EventSystems;

public class CheckCurrentSelected : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private MenuUICursor cursor;
    private GameObject lastSelected;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        lastSelected = EventSystem.current.gameObject;
    }
    void Update()
    {
        if (!menuUI.activeInHierarchy)
        {
            return;
        }
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        cursor.Focus(lastSelected.transform.position);
        if (currentSelected != lastSelected)
        {
            OnFocused(currentSelected);
        }
        if (_inputSetting.GetDecideInputDown())
        {
            OnFocused(lastSelected);
            IPushedObject pushedObject = lastSelected.GetComponent<IPushedObject>();
            pushedObject.OnDecideKeyDown();
        }
        else if (_inputSetting.GetCancelKeyDown())
        {
            OnFocused(lastSelected);
            IPushedObject pushedObject = lastSelected.GetComponent<IPushedObject>();
            pushedObject.OnCancelKeyDown();
        }
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }
    private void OnFocused(GameObject selected)
    {
        if (selected.TryGetComponent<IFocusedObject>(out var focusedObject))
        {
            SoundManager.Instance.PlaySE(0, 5f); //カーソルを動かしたとき
            focusedObject.OnFocused();
        }
    }
}
