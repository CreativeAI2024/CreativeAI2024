using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CloseWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    private GameObjectHolder gameObjectHolder;
    [SerializeField] private GameObject itemWindow;
    [SerializeField] private GameObject previousWindow;
    public GameObject PreviousWindow { set { previousWindow = value; } }
    void Start()
    {
        _inputSetting = InputSetting.Load();
        gameObjectHolder = GameObject.FindWithTag("UIManager").GetComponent<GameObjectHolder>();
    }
    void Update()
    {
        if (_inputSetting.GetCancelKeyDown() && previousWindow != null)
        {
            if (previousWindow == itemWindow)
            {
                CloseToItemWindow();
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
    private void CloseToItemWindow()
    {
        ChangeActive(gameObjectHolder.DisplayButton, false);
        ChangeActive(gameObjectHolder.CombineButton, false);
        gameObject.SetActive(false);
        itemWindow.GetComponent<CloseWindow>().enabled = true;
        Transform itemButtons = itemWindow.transform.GetChild(0);
        itemButtons.GetComponent<MoveCursor>().enabled = true;
        itemButtons.GetComponentsInChildren<Selectable>().ToList().ForEach(selectable => selectable.enabled = true);
        itemButtons.GetComponent<SetFirstButtonFocus>().Focus();
        ChangeActive(previousWindow, true);
    }
    private void ChangeActive(GameObject @object, bool isActive)
    {
        @object.SetActive(isActive);
    }
}
