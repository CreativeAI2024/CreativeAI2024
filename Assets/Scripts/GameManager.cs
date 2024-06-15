using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        // GameManagerのシングルトン設定
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //シーンが移動するときに、BGM変更しとSEを鳴らす
    //シングルトン化しているので、instanceで呼び出す
    public void ToSampleScene()
    {
        SoundManager.instance.PlaySE(0);
        SoundManager.instance.PlayBGM("SampleScene");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBGM("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {

    }
}