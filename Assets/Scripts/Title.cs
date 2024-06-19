using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    private bool firstPush = false;

    public void PressStart()
    {
        Debug.Log("Press Start!");
        if (!firstPush)
        {
            Debug.Log("Go Next Scene!");
            //‚±‚±‚ÉŸ‚ÌƒV[ƒ“‚Ö‚¢‚­–½—ß‚ğ‘‚­B
            SceneManager.LoadScene("SampleScene");
            //
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
