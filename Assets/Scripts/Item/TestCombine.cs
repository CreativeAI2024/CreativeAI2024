using System.Linq;
using UnityEngine;

public class TestCombine : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
    [SerializeField] private Item disc;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        itemInventory.Initialize();
        combineRecipeDatabase.Initialize();
    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            GetDisc();
        }
    }
    private void GetDisc()
    {
        PrintItems("before");
        itemInventory.Add(disc);
        itemInventory.TryCombine(disc);
        PrintItems("after");
    }
    private void PrintItems(string title)
    {
        string returnText = title+": ";
        foreach (string itemName in itemInventory.GetItems().Select(x => x.ItemName))
        {
            returnText += itemName+", ";
        }
        returnText = returnText[..^2];
        Debug.Log(returnText);
    }
}