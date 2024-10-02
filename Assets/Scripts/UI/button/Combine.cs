using UnityEngine;
using UnityEngine.EventSystems;

public class Combine : MonoBehaviour
{
    private InputSetting _inputSetting;
    private ItemList itemList;
    private string itemName;
    public string ItemName {
        set {itemName = value;}
    }
    private CCombine cCombine;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        itemList = Resources.Load<ItemList>("Items/ItemList");
        cCombine = new CCombine(itemList);
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (_inputSetting.GetDecideKeyDown())
            {
                cCombine.Combine(itemName);
            }
        }
    }
}