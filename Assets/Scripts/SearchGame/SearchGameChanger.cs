using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SearchGameChanger : MonoBehaviour
{
    [SerializeField] GameObject[] searchGames;
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
        if(!(FlagManager.Instance.HasFlag("StartSearchGame1")|| FlagManager.Instance.HasFlag("StartSearchGame2")|| FlagManager.Instance.HasFlag("StartSearchGame3"))){
            SceneManager.LoadScene("itemA_room");
        }
    }

    private void SearchGameShifter(string flag, GameObject searchGame)
    {
        if (FlagManager.Instance.HasFlag(flag))
        {
            searchGame.SetActive(true);
        }
    }
}
