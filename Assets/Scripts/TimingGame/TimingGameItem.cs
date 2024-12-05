using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingGameItem : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] ItemInventory itemInventory;


    private void Start()
    {
        itemDatabase.Initialize();
        itemInventory.Initialize();
        itemInventory.Add(itemDatabase.GetItem("BugsInJar"));
        itemInventory.Add(itemDatabase.GetItem("Rei'sBlood"));
        itemInventory.Add(itemDatabase.GetItem("SthFlesh"));
    }

    public void ConsumeItem(int timingGameNumber)
    {
        switch (timingGameNumber)
        {
            case 1:
                ItemDelete("BugsInJar");
                ItemDelete("Rei'sBlood");
                ItemDelete("SthFlesh");
                break;
            case 2:
                ItemDelete("Tessen'sOrgan");
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
