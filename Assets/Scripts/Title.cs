using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    private bool firstPush = false;

    public void PressStart()
    {
        // 参考にした動画　https://www.youtube.com/watch?v=gD0HvOg_i28&t=110s
        //背景の一枚絵は視覚班からもらい次第いれる
        //タイトルはゲームタイトルが決まりしだい、視覚班と相談
        Debug.Log("Press Start!");
        if (!firstPush)
        {
            Debug.Log("Go Next Scene!");
            //ここに次のシーンへいく命令を書く。
            //SapmleSceneにいく事にしている
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
