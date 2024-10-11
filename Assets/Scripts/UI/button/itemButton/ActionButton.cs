using TMPro;
using UnityEngine;

abstract public class ItemButton : MonoBehaviour
{
    protected InputSetting _inputSetting;
    protected GameObjectHolder gameObjectHolder;
    protected ItemInventory itemInventory;
    protected Item thisItem;
    protected OpenWindow openWindow;
    protected bool isOnEnableFirstRun = true;
    protected void Start()
    {
        _inputSetting = InputSetting.Load();
        gameObjectHolder = GameObject.FindWithTag("UIManager").GetComponent<GameObjectHolder>();
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        thisItem = itemInventory.GetItem(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        openWindow = transform.GetComponent<OpenWindow>();
        openWindow.currentWindow = gameObjectHolder.ItemWindow;
    }
}