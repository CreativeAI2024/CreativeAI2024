using UnityEngine;

abstract public class ItemActionButton : MonoBehaviour
{
    protected InputSetting _inputSetting;
    protected GameObjectHolder gameObjectHolder;
    protected ItemInventory itemInventory;
    protected Item thisItem;
    protected OpenWindow openWindow;
    protected bool isOnEnableFirstRun = true;
    public Item ThisItem { set { thisItem = value; } }
    protected void BaseStart()
    {
        
        _inputSetting = InputSetting.Load();
        gameObjectHolder = GameObject.FindWithTag("UIManager").GetComponent<GameObjectHolder>();
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        openWindow = transform.GetComponent<OpenWindow>();
    }
}