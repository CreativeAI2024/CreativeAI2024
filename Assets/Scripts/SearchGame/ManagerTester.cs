using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTester : MonoBehaviour
{
    [SerializeField] private SearchGameManager searchGameManager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            searchGameManager.Activate();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            searchGameManager.Inactivate();
        }
    }
}
