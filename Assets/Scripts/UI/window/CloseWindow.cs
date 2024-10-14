using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CloseWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject itemWindow;
    [SerializeField] private GameObject actionsWindow;
    [SerializeField] private GameObject previousWindow;
    public GameObject PreviousWindow { set { previousWindow = value; } }
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetCancelKeyDown() && previousWindow != null)
        {
            if (gameObject == actionsWindow)
            {
                CloseActionsWindow();
            }
            else
            {
                Close();
            }
        }
    }

    private void Close()
    {
        ChangeActive(gameObject, false);
        ChangeActive(previousWindow, true);
    }
    private void CloseActionsWindow()
    {
        gameObject.SetActive(false);
        itemWindow.GetComponent<CloseWindow>().enabled = true;
        Transform itemButtons = itemWindow.transform.GetChild(0);
        itemButtons.GetComponent<SetFirstButtonFocus>().Focus();
        itemButtons.GetComponent<MoveCursor>().enabled = true;
        itemButtons.GetComponentsInChildren<Selectable>().ToList().ForEach(selectable => selectable.enabled = true);    }
    private void ChangeActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
}
