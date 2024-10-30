using UnityEngine;

public class ItemInitializer : MonoBehaviour
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;

    void Start()
    {
        itemInventory.Initialize();
        combineRecipeDatabase.Initialize();
    }
}
