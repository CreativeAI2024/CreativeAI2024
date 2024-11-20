using UnityEngine;

public class ItemInitializer : MonoBehaviour
{
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;

    void Start()
    {
        itemDatabase.Initialize();
        itemInventory.Initialize();
        combineRecipeDatabase.Initialize();
    }
}
