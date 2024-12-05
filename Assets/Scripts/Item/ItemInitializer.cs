using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

public class ItemInitializer : MonoBehaviour
{
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;

    public void ItemInitialize()
    {
        itemDatabase.Initialize();
        combineRecipeDatabase.Initialize();
        string itemSaveFilePath = string.Join('/', Application.persistentDataPath, "Inventory.dat");
        if (File.Exists(itemSaveFilePath))
        {
            InventoryData inventoryData = SaveUtility.SaveFileToData<InventoryData>(itemSaveFilePath);
            if (inventoryData.Items.Any())
            {
                var items = itemDatabase.GetItems(inventoryData.Items);
                itemInventory.Initialize(items);
                return;
            }
        }
        itemInventory.Initialize();
    }
    
    public void DeleteFlagFile()
    {
        string itemSaveFilePath = string.Join('/', Application.persistentDataPath, "Inventory.dat");//アイテム初期化
        File.Delete(itemSaveFilePath);
        ItemInitialize();
    }
}
