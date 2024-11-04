using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchGameManager : MonoBehaviour
{
    [SerializeField] private List<Renderer> eventTriggerImages;
    private readonly HashSet<Renderer> eventTriggerImageRenderers = new();
    public HashSet<Renderer> EventTriggerImageRenderers => eventTriggerImageRenderers;
    void Start()
    {
        foreach (Renderer renderer in eventTriggerImages)
        {
            eventTriggerImageRenderers.Add(renderer);
        }
    }
}
