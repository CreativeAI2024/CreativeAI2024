using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SoundManagerTester : MonoBehaviour
{
    void Start()
    {
        // ゲーム開始時に最初のBGMを再生
        SoundManager.instance.PlayBGM(1, 1f);
    }

    // シーンが移動するときに、BGMを変更してSEを再生
    public async void ToSampleScene()
    {
        SoundManager.instance.PlaySE(0, 0.5f); 

        // シーンを非同期でロードする
        var loadScene = SceneManager.LoadSceneAsync("SampleScene");
        loadScene.allowSceneActivation = false;

        // シーンのロードが完了するまで待機
        while (loadScene.progress < 0.9f)
        {
            await Task.Yield(); // フレームごとに待機
        }


        // シーンの切り替えを許可
        loadScene.allowSceneActivation = true;

        // シーン切り替え後にBGMを再生（index 0のBGMを再生）
        SoundManager.instance.PlayBGM(0, 0.7f);
    }
}