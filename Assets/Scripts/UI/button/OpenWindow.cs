using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    private ItemInventory itemInventory;
    private GameObjectHolder gameObjectHolder;
    [SerializeField] private GameObject currentWindow;
    [SerializeField] private GameObject nextWindow;
    public GameObject CurrentWindow { set { currentWindow = value; } }
    public GameObject NextWindow { set { nextWindow = value; } }
    void Start()
    {
        _inputSetting = InputSetting.Load();
        gameObjectHolder = GameObject.FindWithTag("UIManager").GetComponent<GameObjectHolder>();
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
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
                else if (gameObjectHolder.ActionsWindow.activeSelf)
                {
                    OpenFromActionsWindow();
                }
                else if (nextWindow == gameObjectHolder.ItemWindow)
                {
                    OpenItemWindow();
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
        gameObjectHolder.ActionButtons.GetComponent<MakeActionButtons>().ThisItem = itemInventory.GetItem(gameObject.name);
        ChangeActive(nextWindow, true);
        currentWindow.GetComponent<CloseWindow>().enabled = false;
        Transform itemButtons = currentWindow.transform.GetChild(0);
        itemButtons.GetComponent<MoveCursor>().enabled = false;
        itemButtons.GetComponentsInChildren<Selectable>().ToList().ForEach(selectable => selectable.enabled = false);
    }
    private void OpenFromActionsWindow()
    {
        ChangeActive(nextWindow, true);
        ChangeEnabled(gameObjectHolder.ActionImageTextButtonComponent, false);
        ChangeEnabled(gameObjectHolder.ActionImageButtonComponent, false);
        ChangeEnabled(gameObjectHolder.ActionTextButtonComponent, false);
        ChangeEnabled(gameObjectHolder.ActionCombineButtonComponent, false);
        ChangeActive(gameObjectHolder.DisplayButton, false);
        ChangeActive(gameObjectHolder.CombineButton, false);
        ChangeActive(gameObjectHolder.ActionsWindow, false);
        ChangeActive(currentWindow, false);
    }
    private void OpenItemWindow()
    {
        ChangeActive(nextWindow, true);
        gameObjectHolder.ItemWindow.GetComponent<CloseWindow>().enabled = true;
        Transform itemButtons = gameObjectHolder.ItemWindow.transform.GetChild(0);
        itemButtons.GetComponent<MoveCursor>().enabled = true;
        itemButtons.GetComponentsInChildren<Selectable>().ToList().ForEach(selectable => selectable.enabled = true);
        ChangeActive(currentWindow, false);
    }
    private void ChangeActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
    private void ChangeEnabled(ActionButton buttonComponent, bool isEnabled)
    {
        buttonComponent.enabled = isEnabled;
    }
}