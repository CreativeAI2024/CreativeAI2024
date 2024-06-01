using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartLoad : MonoBehaviour
{

    private bool firstPush = false;

    public void PressStart()
    {
        Debug.Log("Press Load");
        if (!firstPush)
        {
            Debug.Log("Go Saving Scene!");
            //‚±‚±‚ÉŸ‚ÌƒV[ƒ“‚Ö‚¢‚­–½—ß‚ğ‘‚­
            SceneManager.LoadScene("SampleScene");
            firstPush = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
