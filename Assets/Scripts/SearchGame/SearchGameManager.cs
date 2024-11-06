using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchGameManager : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private SearchGameCursor searchGameCursor;

    public void Activate()
    {
        main.SetActive(true);
    }
    public void Inactivate()
    {
        main.SetActive(false);
        searchGameCursor.Reset();
    }
}
