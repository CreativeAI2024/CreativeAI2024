using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{

    private bool firstPush = false;

    public void PressStart()
    {
        // 参考にした動画　https://www.youtube.com/watch?v=Y3lXlbJhO24

        Debug.Log("Press Load");
        if (!firstPush)
        {
            Debug.Log("Go Saving Scene!");
            //ここに次のシーンへいく命令を書く
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
