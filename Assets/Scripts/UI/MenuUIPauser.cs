using Unity.VisualScripting;
using UnityEngine;

public class MenuUIPauser : MonoBehaviour
{
    [SerializeField] private Pause pause;
    
    void OnEnable()
    {
        pause.PauseAll();
        DebugLogger.Log("PauseAll() called.");
    }

    void OnDisable()
    {
        pause.UnPauseAll();
        DebugLogger.Log("UnPauseAll() called.");
    }
}