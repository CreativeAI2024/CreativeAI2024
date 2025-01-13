using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    void Start()
    {
        DebugLogger.Log("ReachedEnding");
        ConversationTextManager.Instance.InitializeFromJson("Ending");
    }

    void Update()
    {

    }
}
