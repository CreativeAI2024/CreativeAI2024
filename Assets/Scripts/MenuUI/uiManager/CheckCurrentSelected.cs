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
        if (currentSelected == null)
        {
            currentSelected = lastSelected;
        }
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

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
    }
    private void OnFocused(GameObject selected)
    {
        if (selected.TryGetComponent<IFocusedObject>(out var focusedObject))
        {
            SoundManager.Instance.PlaySE(11, 1f); //カーソルを動かしたとき
            focusedObject.OnFocused();
        }
    }
}
