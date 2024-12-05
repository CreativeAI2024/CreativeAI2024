using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObject/Item/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private Item[] allItemList;
    private Dictionary<string, Item> _allItemDictionary;
    
    public void Initialize()
    {
        _allItemDictionary = new Dictionary<string, Item>();
        foreach (Item item in allItemList)
        {
            _allItemDictionary.Add(item.name, item);
        }
    }
    
    public Item GetItem(string item)
    {
        return _allItemDictionary[item];
    }
    
    public IEnumerable<Item> GetItems(IEnumerable<string> items)
    {
        var itemSet = new HashSet<string>(items);
        return allItemList.Where(x => itemSet.Contains(x.name));//fixme
    }
}
