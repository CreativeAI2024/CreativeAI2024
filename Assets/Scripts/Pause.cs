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
    }

    public void PauseAll()
    {
        if (startSceneName == SceneManager.GetActiveScene().name)
        {
            foreach (Behaviour behaviour in pauseScripts)
            {
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
                behaviour.enabled = true;
            }

        }
    }
}
