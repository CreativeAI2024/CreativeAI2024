using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private List<Behaviour> pauseScripts;

    public void PauseAll()
    {
        foreach (Behaviour behaviour in pauseScripts)
        {
            behaviour.enabled = false;
        }
    }

    public void UnPauseAll()
    {
        foreach (Behaviour behaviour in pauseScripts)
        {
            behaviour.enabled = true;
        }
    }
}