using UnityEngine;

public class DatabaseInitialize : MonoBehaviour
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;

    void Start()
    {
        itemInventory.Initialize();
        combineRecipeDatabase.Initialize();
    }
}
