using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private List<Behaviour> pauseScripts;
    private string startSceneName;
    void Start()
    {
        startSceneName = SceneManager.GetActiveScene().name;
        DebugLogger.Log("startSceneName: " + startSceneName);
        string text = "Behaviours: ";
        foreach (Behaviour pauseScript in pauseScripts)
        {
            text += pauseScript.name + ",  ";
        }
        DebugLogger.Log(text);
    }

    public void PauseAll()
    {
        if (startSceneName == SceneManager.GetActiveScene().name)
        {
            foreach (Behaviour behaviour in pauseScripts)
            {
                // DebugLogger.Log("startSceneName: " + startSceneName);
                // DebugLogger.Log("currentSceneName: " + SceneManager.GetActiveScene().name);
                behaviour.enabled = false;
            }

        }
    }

    public void UnPauseAll()
    {
        if (startSceneName == SceneManager.GetActiveScene().name)
        {
            foreach (Behaviour behaviour in pauseScripts)
            {
                // DebugLogger.Log("startSceneName: " + startSceneName);
                // DebugLogger.Log("currentSceneName: " + SceneManager.GetActiveScene().name);
                behaviour.enabled = true;
            }

        }
    }
}
