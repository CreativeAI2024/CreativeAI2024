using UnityEngine;

abstract public class ItemActionButton : MonoBehaviour, IDecideCancelObject
{
    protected ItemInventory itemInventory;
    public Item ThisItem { protected get; set; }
    private void Start()
    {
        itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
        OnStart();
    }

    protected abstract void OnStart();
    public abstract void OnDecideKeyDown();
    public abstract void OnCancelKeyDown();
}