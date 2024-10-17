using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenWindow : MonoBehaviour, IFocusedObject
{
    private ItemInventory itemInventory;
    [SerializeField] private GameObject itemWindow;
    [SerializeField] private GameObject itemWindowButtons;
    [SerializeField] private GameObject actionsWindow;
    [SerializeField] private SetActionButtons actionButtons;
    [SerializeField] private GameObject currentWindow;
    [SerializeField] private GameObject nextWindow;
    [SerializeField] private GameObject displayButton;
    [SerializeField] private GameObject combineButton;
    private ImageTextButton actionImageTextButtonComponent;
    private ImageButton actionImageButtonComponent;
    private TextButton actionTextButtonComponent;
    private CombineButton actionCombineButtonComponent;
    public GameObject NextWindow { set { nextWindow = value; } }

    public void Initialize(GameObject currentWindow, GameObject nextWindow)
    {
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        this.currentWindow = currentWindow;
        this.nextWindow = nextWindow;
        actionImageTextButtonComponent = displayButton.GetComponent<ImageTextButton>();
        actionImageButtonComponent = displayButton.GetComponent<ImageButton>();
        actionTextButtonComponent = displayButton.GetComponent<TextButton>();
        actionCombineButtonComponent = combineButton.GetComponent<CombineButton>();
    }
    private void Open()
    {
        ChangeActive(nextWindow, true);
        ChangeActive(currentWindow, false);
    }
    private void OpenActionsWindow()
    {
        actionButtons.ThisItem = itemInventory.GetItem(gameObject.name);
        ChangeActive(nextWindow, true);
        currentWindow.GetComponent<CloseWindow>().enabled = false;
        Transform itemButtons = currentWindow.transform.GetChild(0);
        itemButtons.GetComponent<MoveCursor>().enabled = false;
        itemButtons.GetComponentsInChildren<Selectable>().ToList().ForEach(selectable => selectable.enabled = false);
    }
    private void OpenFromActionsWindow()
    {
        ChangeActive(nextWindow, true);
        ChangeEnabled(actionImageTextButtonComponent, false);
        ChangeEnabled(actionImageButtonComponent, false);
        ChangeEnabled(actionTextButtonComponent, false);
        ChangeEnabled(actionCombineButtonComponent, false);
        ChangeActive(displayButton, false);
        ChangeActive(combineButton, false);
        ChangeActive(actionsWindow, false);
        ChangeActive(currentWindow, false);
    }
    private void OpenItemWindow()
    {
        ChangeActive(nextWindow, true);
        itemWindow.GetComponent<CloseWindow>().enabled = true;
        itemWindowButtons.GetComponent<MoveCursor>().enabled = true;
        itemWindowButtons.GetComponentsInChildren<Selectable>().ToList().ForEach(selectable => selectable.enabled = true);
        ChangeActive(currentWindow, false);
    }
    private void ChangeActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
    private void ChangeEnabled(ItemActionButton buttonComponent, bool isEnabled)
    {
        buttonComponent.enabled = isEnabled;
    }

    public void OnDecideKeyDown()
    {
        if (nextWindow == actionsWindow)
        {
            OpenActionsWindow();
        }
        else if (actionsWindow.activeSelf)
        {
            OpenFromActionsWindow();
        }
        else if (nextWindow == itemWindow)
        {
            OpenItemWindow();
        }
        else
        {
            Open();
        }
    }
}