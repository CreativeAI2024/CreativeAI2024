using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerImages : MonoBehaviour
{
    private readonly List<GameObject> eventTriggerImageList;
    public List<GameObject> EventTriggerImageList => eventTriggerImageList;
    void Start()
    {
        foreach (GameObject child in transform)
        {
            eventTriggerImageList.Add(child);
        }
    }
}
