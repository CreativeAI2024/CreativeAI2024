using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    private GameObjectHolder gameObjectHolder;
    [SerializeField] private GameObject currentWindow;
    [SerializeField] private GameObject nextWindow;
    public GameObject CurrentWindow { set { currentWindow = value; } }
    public GameObject NextWindow { set { nextWindow = value; } }
    void Start()
    {
        _inputSetting = InputSetting.Load();
        gameObjectHolder = GameObject.FindWithTag("UIManager").GetComponent<GameObjectHolder>();
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                if (nextWindow == gameObjectHolder.ActionsWindow)
                {
                    OpenActionsWindow();
                }
                else if (currentWindow == gameObjectHolder.ActionsWindow)
                {
                    OpenFromActionsWindow();
                }
                else
                {
                    Open();
                }
            }
        }
    }
    private void Open()
    {
        ChangeActive(nextWindow, true);
        ChangeActive(currentWindow, false);
    }
    private void OpenActionsWindow()
    {
        ChangeActive(nextWindow, true);
        currentWindow.GetComponent<CloseWindow>().enabled = false;
        Transform itemButtons = currentWindow.transform.GetChild(0);
        itemButtons.GetComponent<MoveCursor>().enabled = false;
        itemButtons.GetComponentsInChildren<Selectable>().ToList().ForEach(selectable => selectable.enabled = false);
    }
    private void OpenFromActionsWindow()
    {
        ChangeActive(nextWindow, true);
        ChangeActive(gameObjectHolder.ActionsWindow, false);
        ChangeActive(currentWindow, false);
    }
    private void ChangeActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
}