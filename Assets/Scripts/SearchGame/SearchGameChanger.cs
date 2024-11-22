using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchGameChanger : MonoBehaviour
{
    [SerializeField] List<GameObject> searchGames;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var searchGame in searchGames)
        {
            searchGame.SetActive(false);
        }
        if (FlagManager.Instance.HasFlag("StartSearchGame1"))
        {
            searchGames[0].SetActive(true);
        }
        else if (FlagManager.Instance.HasFlag("StartSearchGame2"))
        {
            searchGames[1].SetActive(true);
        }
        else if (FlagManager.Instance.HasFlag("StartSearchGame3"))
        {
            searchGames[2].SetActive(true);
        }
    }
}
