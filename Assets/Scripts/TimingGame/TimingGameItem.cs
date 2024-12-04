using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingGameItem : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] ItemInventory itemInventory;

    public void ConsumeItem(int timingGameNumber)
    {
        switch (timingGameNumber)
        {
            case 1:
                ItemDelete("虫入り瓶");
                ItemDelete("レイの血");
                ItemDelete("ナニカの肉");
                break;
            case 2:
                ItemDelete("テッセンの内臓");
                break;
        }
    }

    private void ItemDelete(string itemName)
    {
        Item item = itemDatabase.GetItem(itemName);
        if (itemInventory.IsContains(item))
        {
            DebugLogger.Log(itemName);
            itemInventory.Remove(item);
        }
    }
}
