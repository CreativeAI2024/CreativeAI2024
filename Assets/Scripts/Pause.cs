using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private List<Behaviour> pauseScripts;
    
    public void PauseAll()
    {
        foreach (Behaviour behaviour in pauseScripts)
        {
            DebugLogger.Log($"Paused Script: {behaviour.name}");
            behaviour.enabled = false;
        }
    }

    public void UnPauseAll()
    {
        foreach (Behaviour behaviour in pauseScripts)
        {
            DebugLogger.Log($"UnPaused Script: {behaviour.name}");
            behaviour.enabled = true;
        }
    }

    public void PauseReset()
    {
        pauseScripts.Clear();
    }
}
