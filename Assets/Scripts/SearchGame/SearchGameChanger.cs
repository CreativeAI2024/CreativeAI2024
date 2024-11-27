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
        SearchGameShifter("StartSearchGame1", searchGames[0]);
        SearchGameShifter("StartSearchGame2", searchGames[1]);
        SearchGameShifter("StartSearchGame3", searchGames[2]);
    }

    private void SearchGameShifter(string flag, GameObject searchGame)
    {
        if (FlagManager.Instance.HasFlag(flag))
        {
            searchGame.SetActive(true);
        }
    }
}
