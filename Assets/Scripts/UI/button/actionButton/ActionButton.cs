using TMPro;
using UnityEngine;

abstract public class ActionButton : MonoBehaviour
{
    protected InputSetting _inputSetting;
    protected GameObjectHolder gameObjectHolder;
    protected ItemInventory itemInventory;
    protected Item thisItem;
    protected OpenWindow openWindow;
    protected bool isOnEnableFirstRun = true;
    public Item ThisItem { set { thisItem = value; } }
    protected void Start()
    {
        _inputSetting = InputSetting.Load();
        gameObjectHolder = GameObject.FindWithTag("UIManager").GetComponent<GameObjectHolder>();
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        openWindow = transform.GetComponent<OpenWindow>();
        openWindow.CurrentWindow = gameObjectHolder.ItemWindow;
    }
}