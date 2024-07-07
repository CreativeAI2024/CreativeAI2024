using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCursor : MonoBehaviour
{
    private int lastRowCount;
    private int columnCount;
    private int rowCount;
    private InputSetting _inputSetting;
    private Selectable focusedButton;
    private int focusedIndex;
    [SerializeField] private Transform cursorTransform;
    void Start()
    {
        columnCount = transform.GetChild(0).GetComponent<GridLayoutGroup>().constraintCount;
        lastRowCount = transform.GetChild(0).childCount%columnCount;
        _inputSetting = InputSetting.Load();
        focusedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        focusedIndex = focusedButton.transform.GetSiblingIndex();
        if (focusedIndex>=columnCount)
        {
            rowCount = transform.GetChild(0).childCount/columnCount + (lastRowCount!=0 ? 1 : 0);
        }
        else
        {
            rowCount = 1;
        }
    }

    void Update()
    {
        if (_inputSetting.GetForwardKeyDown())
        {
            FocusUpButton();
            SetPosition();
        }
        if (_inputSetting.GetBackKeyDown())
        {
            FocusDownButton();
            SetPosition();
        }
        if (_inputSetting.GetLeftKeyDown())
        {
            FocusLeftButton();
            SetPosition();
        }
        if (_inputSetting.GetRightKeyDown())
        {
            FocusRightButton();
            SetPosition();
        }
    }
    private void FocusButton(Selectable focusCandidate)
    {
        if (focusCandidate != null)
        {
            focusedButton = focusCandidate;
            focusedButton.Select();
        }
    }
    private void FocusUpButton()
    {
        if (focusedIndex >= columnCount)
        {
            FocusButton(focusedButton.FindSelectableOnUp());
        }
        else
        {
            if (focusedButton.transform.GetSiblingIndex() >= lastRowCount)
            {
                FocusButton(transform.GetChild(focusedIndex+columnCount*(lastRowCount-2)).GetComponent<Selectable>());
            }
            else 
            {
                //下段
            }
        }
    }

    private void FocusDownButton()
    {
        FocusButton(focusedButton.FindSelectableOnDown());
    }
    private void FocusLeftButton()
    {
        FocusButton(focusedButton.FindSelectableOnLeft());
    }
    private void FocusRightButton()
    {
        FocusButton(focusedButton.FindSelectableOnRight());
    }
    private void SetPosition()
    {
        cursorTransform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
}