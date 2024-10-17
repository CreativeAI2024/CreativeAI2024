using UnityEngine;

abstract public class ItemActionButton : MonoBehaviour, IFocusedObject
{
    protected ItemInventory itemInventory;
    protected OpenWindow openWindow;
    public Item ThisItem { protected get; set; }
    private void Start()
    {
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        openWindow = transform.GetComponent<OpenWindow>();
        OnStart();
    }

    protected abstract void OnStart();
    public abstract void OnDecideKeyDown();
}