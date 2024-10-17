using UnityEngine;

abstract public class ItemActionButton : MonoBehaviour, IFocusedObject
{
    protected InputSetting _inputSetting;
    protected ItemInventory itemInventory;
    protected Item thisItem;
    protected OpenWindow openWindow;
    protected bool isOnEnableFirstRun = true;
    public Item ThisItem { set { thisItem = value; } }
    private void Start()
    {
        
        _inputSetting = InputSetting.Load();
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        openWindow = transform.GetComponent<OpenWindow>();
        OnStart();
    }

    protected abstract void OnStart();
    public abstract void OnDecideKeyDown();
}